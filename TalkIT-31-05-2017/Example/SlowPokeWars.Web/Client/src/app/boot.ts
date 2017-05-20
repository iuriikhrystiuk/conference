import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppComponent } from "./app";
import { GameFieldComponent } from "./game-field";
import "expose-loader?jQuery!jquery";
import "../../../node_modules/signalr/jquery.signalR.js";

@NgModule({
    imports: [BrowserModule],
    declarations: [AppComponent, GameFieldComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }