package drawing.screen {
import flash.display.Shape;

public class DarkLayer extends Shape {

    public function DarkLayer() {
        graphics.beginFill(0x0, 0.8);
        graphics.drawRect(0, 0, 800, 600);
        graphics.endFill();
    }

}
}

