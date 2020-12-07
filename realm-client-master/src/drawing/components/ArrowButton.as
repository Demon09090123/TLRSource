package drawing.components {

import com.company.util.MoreColorUtil;
import drawing.ShapeUtil;

import flash.display.Sprite;
import flash.events.MouseEvent;
import flash.geom.ColorTransform;

import org.osflash.signals.Signal;
import org.osflash.signals.natives.NativeMappedSignal;

public class ArrowButton extends Sprite {

    public var onClick:Signal;

    //0=right,1=up,2=left,3=down
    public function ArrowButton(radius:int, color:uint, direction:int = 0) {
        this.graphics.beginFill(color);
        ShapeUtil.drawPolygon(this.graphics, 0, 0, 3, radius, (90 * direction));
        this.graphics.endFill();

        onClick = new NativeMappedSignal(this, MouseEvent.CLICK);

        addEventListener(MouseEvent.MOUSE_OVER, onOver);
        addEventListener(MouseEvent.MOUSE_OUT, onOut);
    }

    private function onOver(e:MouseEvent):void {
        transform.colorTransform = new ColorTransform(1,220 / 255,133 / 255);
    }

    private function onOut(e:MouseEvent):void {
        transform.colorTransform = MoreColorUtil.identity;
    }
}
}
