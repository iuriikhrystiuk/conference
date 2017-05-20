import { Component, HostListener, NgZone } from "@angular/core";

@Component({
    selector: "my-app",
    template: `
    <h2>My game is: {{gameId}}</h2>

    <div>Error: {{error}}</div>
    <div>
        <button (click)="connect()">Connect</button>
        <button (click)="disconnect()">Disconnect</button>
    </div>
    <game-field [field]="gameState" 
                [self]="self" 
                [opponent]="opponent" 
                [objects]="objects"
                [invert]="invert"></game-field>
  `
})
export class AppComponent {
    gameId: string;
    proxy: any;
    gameState: any;
    self: any;
    opponent: any;
    objects: any;
    error: string;
    invert: boolean;

    constructor(private readonly zone: NgZone) {
        this.onConnected = this.onConnected.bind(this);
        this.gameUpdated = this.gameUpdated.bind(this);
        this.displayError = this.displayError.bind(this);
    }

    public ngOnInit(): any {

        const connection = $.hubConnection("http://localhost:50270");
        connection.logging = true;
        this.proxy = connection.createHubProxy("GameHub");
        this.proxy.on("gameUpdated", this.gameUpdated);
    }

    public connect() {
        // automtic connections will not trigger UI update in angular
        // since it is not using default zone triggers
        this.proxy.connection.start({ jsonp: true }).done(this.onConnected);
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
        this.proxy.invoke("enter", "some name").done(this.gameUpdated).fail(this.displayError);
    }

    private displayError(error: any) {
        this.error = error.message;
    }

    @HostListener("window:beforeunload", ["$event"])
    public onUnload(event) {
        this.proxy.connection.stop();
    }

    @HostListener("window:keydown", ["$event"])
    public onKeypress(event) {
        switch (event.keyCode) {
            case 37:
                this.left();
                break;
            case 39:
                this.right();
                break;
            case 38:
                this.up();
                break;
            case 40:
                this.down();
                break;
            case 32:
                this.fire();
                break;
            default:
                return;
        }

        event.preventDefault();
    }
}