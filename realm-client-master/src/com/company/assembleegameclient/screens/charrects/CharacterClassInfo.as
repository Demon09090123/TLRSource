package com.company.assembleegameclient.screens.charrects {
import com.company.assembleegameclient.objects.ObjectLibrary;
import com.company.ui.SimpleText;

import flash.display.Shape;

import flash.display.Sprite;
import flash.text.TextFieldAutoSize;

import kabam.rotmg.core.model.PlayerModel;


public class CharacterClassInfo extends Sprite {

    private const WIDTH:int = 300;

    private var lines:Shape;

    private var className:SimpleText;
    private var classDescription:SimpleText;


    public function CharacterClassInfo(playerXML:XML) {
        this.className = new SimpleText(24, 0xFFFFFF).setBold(true);
        this.className.text = playerXML.@id;
        this.className.updateMetrics();
        this.className.x = (WIDTH - this.className.width) / 2;

        this.lines = new Shape();
        this.lines.graphics.lineStyle(2, 0xFFFFFF);
        this.lines.graphics.moveTo(0, this.className.height + 5);
        this.lines.graphics.lineTo(WIDTH, this.className.height + 5);

        this.classDescription = new SimpleText(13, 0xFFFFFF, false, WIDTH);
        this.classDescription.multiline = true;
        this.classDescription.wordWrap = true;
        this.classDescription.autoSize = TextFieldAutoSize.CENTER;
        this.classDescription.text = playerXML.Description;
        this.classDescription.updateMetrics();
        this.classDescription.y = 40;

        addChild(this.className);
        addChild(this.lines);
        addChild(this.classDescription);
    }

}
}
