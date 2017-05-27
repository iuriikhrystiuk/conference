import { Component, Input, ElementRef, OnChanges, OnInit } from "@angular/core";

@Component({
    selector: "game-field",
    template: `
    <div>
        <h2>Opponent: {{this.getName(this.opponent)}}</h2>
        <h3>Score: {{this.getPoints(this.opponent)}}</h3>
    </div>
    <canvas></canvas>
    <div>
        <h2>Connected as: {{this.getName(this.self)}}</h2>
        <h3>Score: {{this.getPoints(this.self)}}</h3>
    </div>
  `
})
export class GameFieldComponent implements OnChanges, OnInit {
    @Input() field: any;
    @Input() self: any;
    @Input() opponent: any;
    @Input() objects: any;
    @Input() invert: boolean;
    canvasContext: CanvasRenderingContext2D;
    canvas: HTMLCanvasElement;

    private magnifier: number = 5;

    constructor(private readonly element: ElementRef) {
    }

    ngOnInit(): void {
        this.canvas = this.element.nativeElement.querySelector("canvas") as HTMLCanvasElement;
        this.canvasContext = this.canvas.getContext("2d");
    }

    ngOnChanges(changes: Object): void {
        this.repaint();
    }

    repaint() {
        if (this.field)
            this.paintField(this.field);
        if (this.self) {
            this.canvasContext.fillStyle  = "green";
            this.paintObject(this.self, "fill");
        }
        if (this.opponent) {
            this.canvasContext.fillStyle  = "red";
            this.paintObject(this.opponent, "fill");
        }
        if (this.objects) {
            for (let object of this.objects) {
                if (object.parent === this.self.identifier) {
                    this.canvasContext.strokeStyle = "blue";
                } else {
                    this.canvasContext.strokeStyle = "orange";
                }
                this.paintObject(object, "stroke");
            }
        }
    }

    private paintField(field: any) {
        this.canvas.setAttribute("width", "500");
        this.canvas.setAttribute("height", "500");
    }

    private paintObject(object: any, paintStyle: string) {
        let ordinata = this.invert ? this.invertCoordinates(object.area.bottomRight.y * this.magnifier) : object.area.topLeft.y * this.magnifier;
        let width = (object.area.bottomRight.y - object.area.topLeft.y) * this.magnifier;

        let abscissa = this.invert ? object.area.topLeft.x * this.magnifier : this.invertCoordinates(object.area.bottomRight.x * this.magnifier);
        if (paintStyle === "fill") {
            this.canvasContext.fillRect(
                abscissa,
                ordinata,
                (object.area.bottomRight.x - object.area.topLeft.x) * this.magnifier,
                width);
        }

        if (paintStyle === "stroke") {
            this.canvasContext.strokeRect(
                abscissa,
                ordinata,
                (object.area.bottomRight.x - object.area.topLeft.x) * this.magnifier,
                width);
        }
    }

    private invertCoordinates(y: number): number {
        return 500 - y;
    }

    private getName(player: any) {
        if (player) {
            return player.name;
        }

        return null;
    }

    private getPoints(player: any) {
        if (player) {
            return player.points;
        }

        return null;
    }
}