package kabam.rotmg.ui.characterView
{
import com.company.assembleegameclient.appengine.SavedCharacter;
import com.company.assembleegameclient.objects.ObjectLibrary;
import com.company.assembleegameclient.ui.Stats;
import com.company.assembleegameclient.ui.StatsRadar;
import com.company.ui.SimpleText;

import drawing.components.TitleMenuOption;

import flash.display.BitmapData;
import flash.display.Sprite;
import flash.events.MouseEvent;

import kabam.rotmg.assets.services.CharacterFactory;
import kabam.rotmg.classes.model.CharacterClass;
import kabam.rotmg.classes.model.CharacterSkin;

import kabam.rotmg.classes.model.ClassesModel;

import kabam.rotmg.core.StaticInjectorContext;

import kabam.rotmg.core.model.PlayerModel;

import org.osflash.signals.Signal;
import org.osflash.signals.natives.NativeMappedSignal;
import org.swiftsuspenders.Injector;

public class CurrentCharacterScreen extends Sprite
   {
      public var playerModel:PlayerModel;

      public var onPlay:Signal;
      public var onClose:Signal;

      private var titleTxt:SimpleText;
      private var icon:Sprite;
      private var statView:Stats;
      private var radar:StatsRadar;

      private var nameTxt:SimpleText;
      private var classTxt:SimpleText;
      private var expTxt:SimpleText;

      private var backButton:TitleMenuOption;


      public function CurrentCharacterScreen(model:PlayerModel) {
          this.playerModel = model;
          makeCharView();
          setIcon();
          makeButtons();
      }

      private function makeButtons() : void {
          this.backButton = new TitleMenuOption("Back");

          this.backButton.x = 10;
          this.backButton.y = 560;

          addChild(this.backButton);

          this.onClose = new NativeMappedSignal(this.backButton, MouseEvent.CLICK);
      }

      private function makeCharView() : void {
          var curXML:XML = this.playerModel.getSavedCharacter().getStats();

          this.radar = new StatsRadar();
          this.radar.addStatsRadar([
              int(curXML.Attack),
              (int(curXML.PhysicalDefense) + int(curXML.MagicDefense)) / 2,
              (int(curXML.HpRegen) +  int(curXML.MpRegen)) / 2,
              int(curXML.MagicPower),
              int(curXML.Speed),
              int(curXML.Dexterity)
          ]);
          this.radar.x = 140;
          this.radar.y = 150;

          this.statView = new Stats(150, 200);
          this.statView.drawXML(curXML);
          this.statView.x = 40;
          this.statView.y = 310;

          addChild(this.radar);
          addChild(this.statView);
      }

      public function setIcon() : void
      {
          var savedChar:SavedCharacter = this.playerModel.getSavedCharacter();
          var injector:Injector = StaticInjectorContext.getInjector();
          var type:CharacterClass = injector.getInstance(ClassesModel).getCharacterClass(savedChar.objectType());
          var skin:CharacterSkin = type.skins.getSkin(savedChar.skinType()) || type.skins.getDefaultSkin();
          var data:BitmapData = injector.getInstance(CharacterFactory).makeIcon(skin.template,100,savedChar.tex1(),savedChar.tex2());

         this.icon = new Sprite();
         this.icon.graphics.beginBitmapFill(data);
         this.icon.graphics.drawRect(0, 0, data.width, data.height);
         this.icon.scaleX = 5.5;
         this.icon.scaleY = 5.5;
         this.icon.alpha = .8;
         this.icon.x = (CharacterScreenInfo.WIDTH - this.icon.width) / 2;
         this.icon.y = CharacterScreenInfo.HEIGHT - this.icon.height - 10;

         this.icon.addEventListener(MouseEvent.MOUSE_OVER, onMouseOver);
         this.icon.addEventListener(MouseEvent.MOUSE_OUT, onRollOut);
         this.onPlay = new NativeMappedSignal(this.icon, MouseEvent.CLICK);

         addChild(this.icon);

         addText();
      }

      private function addText():void {
          var savedChar:SavedCharacter = this.playerModel.getSavedCharacter();

          this.titleTxt = new SimpleText(38, 0xFFFFFF).setBold(true);
          this.nameTxt = new SimpleText(24, 0xFFFFFF).setBold(true);
          this.classTxt = new SimpleText(18, 0xFFFFFF).setBold(true);
          this.expTxt = new SimpleText(16, 0xFFFFFF);

          this.nameTxt.text = this.playerModel.getName();
          this.classTxt.text = "Level" + " " + savedChar.level() + " " + ObjectLibrary.getIdFromType(savedChar.objectType());
          this.expTxt.text = savedChar.xp() + "/" + savedChar.nextLvlXP();
          this.titleTxt.text = "Last Realm";

          this.titleTxt.updateMetrics();
          this.titleTxt.addOutline(3);
          this.nameTxt.updateMetrics();
          this.nameTxt.addOutline();
          this.classTxt.updateMetrics();
          this.expTxt.updateMetrics();

          this.titleTxt.x = (CharacterScreenInfo.WIDTH - this.titleTxt.width) / 2;
          this.titleTxt.y = 10;
          this.nameTxt.x = (CharacterScreenInfo.WIDTH - this.nameTxt.width) / 2;
          this.nameTxt.y = (this.icon.y) - 60;
          this.classTxt.x = (CharacterScreenInfo.WIDTH - this.classTxt.width) / 2;
          this.classTxt.y = (this.icon.y ) - 35;
          this.expTxt.x = (CharacterScreenInfo.WIDTH - this.expTxt.width) / 2;
          this.expTxt.y = (this.icon.y ) - 15;

          addChild(this.titleTxt);
          addChild(this.nameTxt);
          addChild(this.classTxt);
          addChild(this.expTxt);
      }

      private function onMouseOver(event:MouseEvent) : void
      {
          this.icon.alpha = 1;
      }
      private function onRollOut(event:MouseEvent) : void
      {
          this.icon.alpha = .8;
      }
   }
}
