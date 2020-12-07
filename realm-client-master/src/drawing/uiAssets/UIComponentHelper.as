package drawing.uiAssets {
import com.company.util.BitmapUtil;

import drawing.uiAssets.characterScreen.CharacterScreenBG;
import drawing.uiAssets.characterScreen.CharacterScreenSpawn;
import drawing.uiAssets.characterScreen.CharacterStatIcon;
import drawing.uiAssets.welcomeScreen.WelcomeScreenBG;

import flash.display.Bitmap;
import flash.display.BitmapData;

public class UIComponentHelper {
    public static var _welcomeScreen:Bitmap;
    public static var _charScreen:Bitmap;
    public static var _charScreenSpawn:Bitmap;
    private static var _charStatIcons:Bitmap;

    public static function load():void {
        _welcomeScreen = new WelcomeScreenBG();
        _charScreen = new CharacterScreenBG();

        _charScreenSpawn = new CharacterScreenSpawn();
        _charStatIcons = new CharacterStatIcon();
    }

    public static function getCharStatIcons(x:int):BitmapData {
        return BitmapUtil.cropToBitmapData(_charStatIcons.bitmapData, x * 30, 0, 30, 30);
    }

    public function UIComponentHelper() {
    }
}
}
