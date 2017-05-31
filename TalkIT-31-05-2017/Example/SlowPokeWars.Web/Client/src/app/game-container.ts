import { Component, HostListener, NgZone } from "@angular/core";

@Component({
    selector: "game-container",
    template: `
    <div [style.display]="!!(gameId) ? 'none' : 'block'">
            <h1>Welcome to Slowpoke Wars!</h1>
            <h2>Enter your name to connect to game:</h2>
            <input type="text" [(ngModel)]="name" class="form-control"/>
            <div>
                <button (click)="connect()" class="btn btn-primary" [disabled]="!(name)">Connect</button>
            </div>
    </div>
    <div [style.display]="!(gameId) ? 'none' : 'block'">
            <h2>Status: {{getStatus()}}</h2>
            <div>
                <button (click)="disconnect()" class="btn btn-secondary">Disconnect</button>
            </div>
            <game-field [field]="gameState" 
                        [self]="self" 
                        [opponent]="opponent" 
                        [objects]="objects"
                        [invert]="invert"></game-field>
    </div>
  `
})
export class GameContainerComponent {
    gameId: string = null;
    proxy: any;
    gameState: any;
    self: any;
    opponent: any;
    objects: any;
    error: string;
    invert: boolean;
    name: string = null;

    constructor(private readonly zone: NgZone) {
        this.onConnected = this.onConnected.bind(this);
        this.gameUpdated = this.gameUpdated.bind(this);
        this.displayError = this.displayError.bind(this);
        this.onKeypress = this.onKeypress.bind(this);
        this.up = this.up.bind(this);
        this.down = this.down.bind(this);
        this.right = this.right.bind(this);
        this.left = this.left.bind(this);
        this.fire = this.fire.bind(this);
    }

    public ngOnInit(): any {
        const connection = $.hubConnection();
        connection.logging = true;
        this.proxy = connection.createHubProxy("GameHub");
        this.proxy.on("gameUpdated", this.gameUpdated);
    }

    public connect() {
        // automatic connections will not trigger UI update in angular
        // since it is not using default zone triggers
        this.proxy.connection.start({ jsonp: true }).done(this.onConnected);

        this.proxy.connection.reconnecting(() => {
        });

        this.proxy.connection.reconnected(() => {
        });

        this.proxy.connection.disconnected(() => {
        });

        this.proxy.connection.disconnected(()=> {
            setTimeout(() => {
                this.proxy.connection.start().done(this.onConnected);
            }, 5000); // Restart connection after 5 seconds.
        });
    }

    public left() {
        this.proxy.invoke("moveLeft", this.gameId);
    }

    public right() {
        this.proxy.invoke("moveRight", this.gameId);
    }

    public up() {
        this.proxy.invoke("moveUp", this.gameId);
    }

    public down() {
        this.proxy.invoke("moveDown", this.gameId);
    }

    public disconnect() {
        this.proxy.invoke("leave").done(this.gameUpdated);
    }

    public fire() {
        this.proxy.invoke("fire", this.gameId);
    }

    public getStatus() {
        if (this.gameId) {
            if (this.opponent) {
                return "At war!";
            } else {
                return "Awaiting opponent";
            }
        }

        return "Not connected";
    }

    private gameUpdated(gameDescription: any) {
        if (gameDescription) {
            let field = gameDescription.field;
            this.gameId = gameDescription.id;
            if (field.top && field.top.identifier === this.proxy.connection.id) {
                this.self = field.top;
                this.opponent = field.bottom;
                this.invert = false;
            } else if (field.bottom && field.bottom.identifier === this.proxy.connection.id) {
                this.self = field.bottom;
                this.opponent = field.top;
                this.invert = true;
            } else {
                this.resetSatate();
            }
            this.gameState = gameDescription;
            this.objects = field.objects;
        } else {
            this.resetSatate();
        }
    }

    private resetSatate() {
        this.gameId = null;
        this.self = null;
        this.opponent = null;
        this.objects = null;
        this.gameState = null;
    }

    private onConnected() {
        this.proxy.invoke("enter", this.name).done(this.gameUpdated).fail(this.displayError);
    }

    private displayError(error: any) {
        this.error = error.message;
    }

    private doAction(action) {
        if (this.opponent) {
            action();
        }
    }

    @HostListener("window:beforeunload", ["$event"])
    public onUnload(event) {
        this.proxy.connection.stop();
    }

    @HostListener("window:keydown", ["$event"])
    public onKeypress(event) {
        switch (event.keyCode) {
            case 37:
                this.doAction(this.left);
                break;
            case 39:
                this.doAction(this.right);
                break;
            case 38:
                this.doAction(this.up);
                break;
            case 40:
                this.doAction(this.down);
                break;
            case 32:
                this.doAction(this.fire);
                break;
            default:
                return;
        }

        event.preventDefault();
    }
}