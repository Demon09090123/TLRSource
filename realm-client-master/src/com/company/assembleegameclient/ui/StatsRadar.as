package com.company.assembleegameclient.ui {
import avmplus.typeXml;

import com.company.ui.SimpleText;
import com.company.util.GraphicsUtil;

import drawing.ShapeUtil;

import flash.display.Shape;

import flash.display.Sprite;
import flash.geom.Point;

public class StatsRadar extends Sprite {

    public static const RADAR_STATS:Array = [
            "Attack", "Defense", "Sustain", "Magic", "Speed", "Dexterity"
    ];

    private const MAX_LENGTH:int = 100;

    private var textColor:uint;
    private var bgColor:uint;
    private var lineColor:uint;

    public function StatsRadar(bgClr:uint = 0x363636, txtClr:uint = 0xFFFFFF, lineClr:uint = 0) {
        this.textColor = txtClr;
        this.bgColor = bgClr;
        this.lineColor = lineClr;

        createRadarBG();
    }

    public function addStatsRadar(statData:Array):void {
        var radar:Shape = new Shape();
        var step:Number = (Math.PI * 2) / RADAR_STATS.length;

        radar.graphics.beginFill(0xFFFFFF, .7);
        radar.graphics.lineStyle(2, this.lineColor);
        radar.graphics.moveTo(Math.cos(0) * statData[0], 0 - Math.sin(0) * statData[0]);

        var x:Number, y:Number;

        for (var irr:int = 1; irr <= RADAR_STATS.length; ++irr) {
            var angle:Number = step * irr;
            if (irr < 6) {
                x = Math.cos(angle) * statData[irr];
                y = 0 - Math.sin(angle) * statData[irr];
            } else {
                x = Math.cos(0) * statData[0];
                y = 0 - Math.sin(0) * statData[0];
            }

            radar.graphics.lineTo(x, y);
        }

        addChild(radar);
    }

    private function getRandom():Number {
        return Math.random() * 100;
    }

    private function createRadarBG():void {
        var bg:Sprite = new Sprite();
        bg.graphics.beginFill(this.bgColor, 0.8);
        bg.graphics.lineStyle(2, this.lineColor);
        var points:Vector.<Point> = ShapeUtil.drawPolygon(bg.graphics, 0, 0, RADAR_STATS.length, MAX_LENGTH);
        bg.graphics.endFill();

        var idx:int = 0;
        for each(var p:Point in points) {
            if (idx == RADAR_STATS.length) {
                break;
            }

            var line:Shape = new Shape();
            line.graphics.lineStyle(2, this.lineColor);
            line.graphics.moveTo(0, 0);
            line.graphics.lineTo(p.x, p.y);

            var pTxt:SimpleText = new SimpleText(12, this.textColor);
            pTxt.text = RADAR_STATS[idx];
            pTxt.updateMetrics();

            if (p.x < 0) {
                p.x -= pTxt.width;
            }
            if (Math.abs(p.y) == this.MAX_LENGTH) {
                p.x -= pTxt.width / 2;
            }
            if (p.y < 0) {
                p.y -= pTxt.height;
            }

            pTxt.x = p.x;
            pTxt.y = p.y;

            bg.addChild(line);
            bg.addChild(pTxt);

            idx++;
        }

        addChild(bg);
    }
}
}
