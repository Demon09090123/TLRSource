package kabam.rotmg.ui.characterView
{
import com.company.assembleegameclient.appengine.SavedCharacter;
import com.company.assembleegameclient.objects.ObjectLibrary;
import com.company.assembleegameclient.ui.StatsRadar;
import com.company.assembleegameclient.util.AnimatedChar;
import com.company.ui.SimpleText;

import drawing.components.ArrowButton;
import drawing.components.TitleMenuOption;

import flash.display.Bitmap;
import flash.display.BitmapData;
import flash.display.Shape;
import flash.display.Sprite;
import flash.events.MouseEvent;
import flash.filters.GlowFilter;

import org.osflash.signals.Signal;

public class NewCharacterScreen extends Sprite
{
    private const defaultClass:Vector.<XML> = ObjectLibrary.playerChars_;

    private var titleTxt:SimpleText;
    private var titleLine:Shape;
    private var charInfoBar:CharacterClassInfo;
    private var statRadar:StatsRadar;
    private var selectBtn:TitleMenuOption;
    private var leftBtn:ArrowButton;
    private var rightBtn:ArrowButton;
    private var btnBG:Shape;

    private var curIndex:int;

    public var onCreate:Signal;

    public function NewCharacterScreen()
    {
        drawButtons();
        setSelected(int(Math.random() * defaultClass.length));
    }

    private function drawButtons():void {
        this.titleTxt = new SimpleText(24, 0xFFFFFF).setBold(true);
        this.titleLine = new Shape();
        this.selectBtn = new TitleMenuOption("CREATE", 18, false, true);
        this.leftBtn = new ArrowButton(15, 0xFFFFFF, 2);
        this.rightBtn = new ArrowButton(15, 0xFFFFFF, 0);
        this.btnBG = new Shape();

        this.onCreate = new Signal(uint);
        this.selectBtn.addEventListener(MouseEvent.CLICK, onSelect);
        this.leftBtn.onClick.add(onLeft);
        this.rightBtn.onClick.add(onRight);
        this.titleTxt.text = "Create Character";
        this.titleTxt.updateMetrics();
        this.titleLine = new Shape();
        this.titleLine.graphics.lineStyle(2, 0xFFFFFF);
        this.titleLine.graphics.moveTo(0, 45);
        this.titleLine.graphics.lineTo(CharacterScreenInfo.WIDTH, 45);
        this.btnBG.graphics.beginFill(0, .8);
        this.btnBG.graphics.drawRect(0, 0, CharacterScreenInfo.WIDTH, 40);

        this.titleTxt.x = (CharacterScreenInfo.WIDTH - this.titleTxt.width) / 2;
        this.titleTxt.y = 10;

        this.selectBtn.x = (CharacterScreenInfo.WIDTH - this.selectBtn.width) / 2;
        this.selectBtn.y = 483;
        this.leftBtn.x = (this.selectBtn.x - (this.leftBtn.width / 2) - 5);
        this.leftBtn.y = 494;
        this.rightBtn.x = (this.selectBtn.x + this.selectBtn.width) + 15;
        this.rightBtn.y = 494;
        this.btnBG.y = 475;

        addChild(this.titleTxt);
        addChild(this.btnBG);
        addChild(this.titleLine);
        addChild(this.selectBtn);
        addChild(this.leftBtn);
        addChild(this.rightBtn);
    }

    private function makeAccBar():void {
        if (this.charInfoBar != null) {
            removeChild(this.charInfoBar);
        }
        if (this.statRadar != null) {
            removeChild(this.statRadar);
        }

        this.charInfoBar = new CharacterClassInfo(this.defaultClass[this.curIndex]);
        this.charInfoBar.x = 5;
        this.charInfoBar.y = 70;

        var curXML:XML = this.defaultClass[this.curIndex];

        this.statRadar = new StatsRadar();
        this.statRadar.addStatsRadar([
            int(curXML.Attack) * 5,
            Math.max(int(curXML.PhysicalDefense), int(curXML.MagicDefense)) * 5,
            Math.max(int(curXML.HpRegen), int(curXML.MpRegen)) * 5,
            int(curXML.MagicPower) * 5,
            int(curXML.Speed) * 5,
            int(curXML.Dexterity) * 5
        ]);

        this.statRadar.x = 650;
        this.statRadar.y = 170;

        addChild(this.charInfoBar);
        addChild(this.statRadar);
    }

    private function onSelect(e:MouseEvent):void {
        this.onCreate.dispatch(uint(this.defaultClass[this.curIndex].@type));
    }

    private function onLeft():void {
        setSelected(getLeftIndex());
    }

    private function onRight():void {
        setSelected(getRightIndex());
    }


    private var centerBitmap:Bitmap;
    private var leftBitmap:Bitmap;
    private var rightBitmap:Bitmap;

    private function setSelected(index:int):void {
        removeDraw();

        this.curIndex = index;
        var cXml:XML = defaultClass[this.curIndex];
        var lXml:XML = defaultClass[getLeftIndex()];
        var rXml:XML = defaultClass[getRightIndex()];

        this.centerBitmap = getBitmap(cXml, false, true);
        this.leftBitmap = getBitmap(lXml, true, false);
        this.rightBitmap = getBitmap(rXml, true, false);

        this.centerBitmap.x = (CharacterScreenInfo.WIDTH - this.centerBitmap.width) / 2 + 12;
        this.centerBitmap.y = 280;

        this.leftBitmap.x = (this.centerBitmap.x + 15) - this.leftBitmap.width;
        this.rightBitmap.x = this.centerBitmap.x + (this.centerBitmap.width - 15);
        this.leftBitmap.y = 310;
        this.rightBitmap.y = 310;

        addChild(this.leftBitmap);
        addChild(this.centerBitmap);
        addChild(this.rightBitmap);

        makeAccBar();
    }

    private function getLeftIndex():int {
        if (curIndex == 0) {
            return defaultClass.length - 1;
        }
        return curIndex - 1;
    }

    private function getRightIndex():int {
        if (curIndex == defaultClass.length - 1) {
            return 0;
        }
        return curIndex + 1;
    }

    private function removeDraw():void {
        if (this.centerBitmap != null) {
            removeChild(this.centerBitmap);
        }
        if (this.leftBitmap != null) {
            removeChild(this.leftBitmap);
        }
        if (this.rightBitmap != null) {
            removeChild(this.rightBitmap);
        }
    }

    private function getBitmap(xml:XML, isDimmed:Boolean = true, isSelected:Boolean = false):Bitmap {
        var bData:BitmapData = SavedCharacter.getImage(null, xml, AnimatedChar.DOWN, AnimatedChar.STAND, 0, false, isDimmed);
        var bitmap:Bitmap = new Bitmap(bData);
        bitmap.filters = [new GlowFilter(0xFFFFFF, 1, 3, 3, 5, 1)];

        bitmap.scaleX = 2;
        bitmap.scaleY = 2;
        if (isSelected) {
            bitmap.scaleX = 3;
            bitmap.scaleY = 3;
        }

        return bitmap;
    }


}
}
