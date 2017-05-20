import { Component, Input, ElementRef, OnChanges, OnInit } from "@angular/core";

@Component({
    selector: "game-field",
    template: `
    <canvas></canvas>
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

    private magnifier: number = 3;

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
            this.canvasContext.strokeStyle  = "green";
            this.paintObject(this.self);
        }
        if (this.opponent) {
            this.canvasContext.strokeStyle  = "red";
            this.paintObject(this.opponent);
        }
        if (this.objects) {
            this.canvasContext.strokeStyle = "blue";
            for (let object of this.objects) {
                this.paintObject(object);
            }
        }
    }

    private paintField(field: any) {
        this.canvas.setAttribute("width", "310");
        this.canvas.setAttribute("height", "310");
    }

    private paintObject(object: any) {
        let ordinata = this.invert ? this.invertOrdinata(object.area.topLeft.y * this.magnifier) : object.area.topLeft.y * this.magnifier;
        let width = (object.area.bottomRight.y - object.area.topLeft.y) * this.magnifier;
        this.canvasContext.strokeRect(
            object.area.topLeft.x * this.magnifier,
            ordinata + (this.invert ? -width : 0),
            (object.area.bottomRight.x - object.area.topLeft.x) * this.magnifier,
            width);
    }

    private invertOrdinata(y: number): number {
        return 305 - y;
    }
}