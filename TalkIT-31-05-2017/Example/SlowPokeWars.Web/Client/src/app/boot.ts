import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from '@angular/forms';
import { AppComponent } from "./app";
import { GameFieldComponent } from "./game-field";
import { GameContainerComponent } from "./game-container";
import "expose-loader?jQuery!jquery";
import "../../../node_modules/signalr/jquery.signalR.js";
import "!style-loader!css-loader!../../styles/App.css";

@NgModule({
    imports: [BrowserModule, FormsModule],
    declarations: [AppComponent, GameFieldComponent, GameContainerComponent],
    bootstrap: [AppComponent],
})
export class AppModule { }