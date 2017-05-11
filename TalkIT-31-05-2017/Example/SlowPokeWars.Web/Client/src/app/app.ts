import { Component, HostListener, NgZone } from "@angular/core";
import Rx from "rxjs";

@Component({
    selector: "my-app",
    template: `
    <h2>My game is: {{gameId}}</h2>
  `
})
export class AppComponent {
    gameId: string;
    proxy: any;
    gameIdObserver: Rx.Subject<string> = new Rx.Subject<string>();

    constructor(private readonly zone:NgZone) {
        this.onConnected = this.onConnected.bind(this);
        this.onGameEntered = this.onGameEntered.bind(this);
        this.gameIdObserver.subscribe((id: string) => { this.gameId = id; });
    }

    public ngOnInit(): any {

        const connection = $.hubConnection("http://localhost:50270");
        connection.logging = true;
        this.proxy = connection.createHubProxy("GameHub");
        this.proxy.on("gameUpdated", () => { });

        connection.start({ jsonp: true }).done(this.onConnected);
    };

    private onConnected() {
        this.proxy.invoke("enter", "some name").done(this.onGameEntered);
    };

    private onGameEntered(id: string) {
        this.gameIdObserver.next(id);
    }

    @HostListener('window:unload', ['$event'])
    public onUnload(event) {
        this.proxy.connection.stop();
    };
}