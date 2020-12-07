package com.company.assembleegameclient.screens.charrects
{
   import com.company.assembleegameclient.appengine.SavedCharacter;
import com.company.assembleegameclient.objects.ObjectLibrary;
import com.company.assembleegameclient.screens.CharacterList;
import com.company.assembleegameclient.ui.Stats;
import com.company.assembleegameclient.ui.StatsRadar;
import com.company.assembleegameclient.ui.StatusBar;
import com.company.assembleegameclient.ui.tooltip.MyPlayerToolTip;
   import com.company.assembleegameclient.ui.tooltip.ToolTip;
   import com.company.assembleegameclient.util.FameUtil;
import com.company.assembleegameclient.util.RandomUtil;
import com.company.assembleegameclient.util.redrawers.GlowRedrawer;
import com.company.rotmg.graphics.StarGraphic;
   import com.company.ui.SimpleText;
import com.company.util.Random;

import drawing.ShapeUtil;

import drawing.uiAssets.UIComponentHelper;

import flash.display.Bitmap;

import flash.display.Bitmap;
import flash.display.DisplayObject;
import flash.display.Shape;
import flash.display.Sprite;
   import flash.events.Event;
   import flash.events.MouseEvent;
   import flash.filters.DropShadowFilter;
   import flash.geom.ColorTransform;
import flash.geom.Point;

import kabam.rotmg.classes.model.CharacterClass;
import kabam.rotmg.core.model.PlayerModel;

import org.osflash.signals.Signal;
   import org.osflash.signals.natives.NativeMappedSignal;

import starling.utils.MathUtil;

public class CurrentCharacterRect extends Sprite
   {
      public var playerModel:PlayerModel;

      private var icon:DisplayObject;
      private var charView:CharacterAccountBar;
      private var charClass:CharacterClassInfo;
      private var radar:StatsRadar;

      private var spawnImage:Bitmap;

      public function CurrentCharacterRect(model:PlayerModel) {
          this.playerModel = model;
          makeCharView();
          makeSpawn();
      }

      private function makeCharView():void {
          this.charView = new CharacterAccountBar();
          this.charView.y = 10;

          this.charClass = new CharacterClassInfo(ObjectLibrary.xmlLibrary_[this.playerModel.getSavedCharacter().objectType()]);
          this.charClass.x = 500;
          this.charClass.y = 10;

          this.radar = new StatsRadar();
          this.radar.x = 650;
          this.radar.y = 250;

          this.charView.setText(this.playerModel.getName(), this.playerModel.getLevel().toString(), this.playerModel.getCredits());

          addChild(this.charView);
          addChild(this.charClass);
          addChild(this.radar);
      }

      private function makeSpawn(): void {
          this.spawnImage = UIComponentHelper._charScreenSpawn;
          this.spawnImage.x  = (CharacterList.WIDTH - this.spawnImage.width) / 2;
          this.spawnImage.y = 185;

          addChild(this.spawnImage);
      }

      public function setIcon(value:DisplayObject) : void
      {
         this.icon && removeChild(this.icon);
         this.icon = value;
         this.icon.scaleX = 2;
         this.icon.scaleY = 2;
         this.icon.x = (CharacterList.WIDTH - this.icon.width) / 2;
         this.icon.y = 125;

         this.icon && addChild(this.icon);
      }

      protected function onMouseOver(event:MouseEvent) : void
      {
          if (this.icon) {
              this.icon.alpha = .5;
          }
      }

      protected function onRollOut(event:MouseEvent) : void
      {
          if (this.icon) {
              this.icon.alpha = 1;
          }
      }
   }
}
