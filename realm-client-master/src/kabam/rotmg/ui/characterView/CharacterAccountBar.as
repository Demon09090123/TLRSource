package kabam.rotmg.ui.characterView {
import com.company.assembleegameclient.appengine.SavedCharacter;
import com.company.ui.SimpleText;
import com.company.util.GraphicsUtil;

import drawing.uiAssets.UIComponentHelper;

import flash.display.Bitmap;
import flash.display.GraphicsPath;
import flash.display.GraphicsSolidFill;
import flash.display.IGraphicsData;
import flash.display.Shape;

import flash.display.Sprite;

import kabam.rotmg.core.model.PlayerModel;

import kabam.rotmg.game.view.CreditDisplay;

public class CharacterAccountBar extends Sprite {

    private var nameText:SimpleText;
    private var levelText:SimpleText;
    private var creditDisplay:CreditDisplay;

    private var infoBG:Shape;


    private function addBG():void {
        var fill:GraphicsSolidFill = new GraphicsSolidFill(0x9E9E9E);
        var  path:GraphicsPath = new GraphicsPath(new Vector.<int>(),new Vector.<Number>());
        var gData:Vector.<IGraphicsData> = new <IGraphicsData>
                [fill, path, GraphicsUtil.END_FILL, GraphicsUtil.END_STROKE];

        GraphicsUtil.drawCutEdgeRect(0, 0, 350, 50, 50, [0, 0, 1, 0], path);
        graphics.drawGraphicsData(gData);

        GraphicsUtil.clearPath(path);
        fill = new GraphicsSolidFill(0xD3D3D3);
        gData = new <IGraphicsData>
        [fill, path, GraphicsUtil.END_FILL, GraphicsUtil.END_STROKE];

        this.infoBG = new Shape();
        GraphicsUtil.drawCutEdgeRect(0, 50, 250, 25, 25, [0, 0, 1, 0], path);
        this.infoBG.graphics.drawGraphicsData(gData);
        addChild(infoBG);
    }

    public function setText(nameText:String, level:String, credits:int = -1):void {
        this.nameText.text = nameText.toUpperCase();
        this.nameText.updateMetrics();
        this.levelText.text = "Level " + level;
        this.levelText.updateMetrics();

        this.nameText.x = 5;
        this.nameText.y = (50 - this.nameText.height) / 2;
        this.levelText.y = 50 + ((25 - this.levelText.height) / 2);
        this.levelText.x = 5;

        addChild(this.nameText);
        addChild(this.levelText);

        if (credits != -1) {
            this.creditDisplay.draw(credits);

            this.creditDisplay.y = 5;
            this.creditDisplay.x = 300;

            addChild(this.creditDisplay);
        }
    }

    public function CharacterAccountBar() {
        addBG();

        this.nameText = new SimpleText(30, 0x212121);
        this.nameText.setBold(true);

        this.levelText = new SimpleText(18, 0x515151);

        this.creditDisplay = new CreditDisplay();
    }
}
}
