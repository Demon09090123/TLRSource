package kabam.rotmg.assets.model {
import flash.display.BitmapData;


public class AssetTemplate {

    public var Data:BitmapData;
    public var sizeX:int;
    public var sizeY:int;

    public function AssetTemplate(c:BitmapData, x:int, y:int) {
        this.Data = c;
        this.sizeX = x;
        this.sizeY = y;
    }
}


}
