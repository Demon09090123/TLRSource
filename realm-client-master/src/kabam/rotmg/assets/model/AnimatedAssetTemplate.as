package kabam.rotmg.assets.model {
import com.company.assembleegameclient.util.AnimatedChar;

import flash.display.BitmapData;


public class AnimatedAssetTemplate {
    public var Data:BitmapData;
    public var Mask:BitmapData;
    public var sizeX:int;
    public var sizeY:int;
    public var width:int;
    public var height:int;
    public var dir:int;

    public function AnimatedAssetTemplate(c:BitmapData,mc:BitmapData, x:int, y:int, width:int, height:int, dir:int) {
        this.Data = c;
        this.Mask = mc;
        this.sizeX = x;
        this.sizeY = y;
        this.dir = dir;
    }

}


}
