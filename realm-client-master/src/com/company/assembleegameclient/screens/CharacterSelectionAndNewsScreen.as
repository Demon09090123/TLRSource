package com.company.assembleegameclient.screens
{
   import com.company.assembleegameclient.ui.ClickableText;
   import com.company.assembleegameclient.ui.Scrollbar;
   import com.company.rotmg.graphics.ScreenGraphic;
   import com.company.ui.SimpleText;
import com.hurlant.util.asn1.parser.nulll;

import flash.display.DisplayObject;
   import flash.display.Shape;
   import flash.display.Sprite;
   import flash.events.Event;
   import flash.events.MouseEvent;
   import flash.filters.DropShadowFilter;
   import flash.geom.Rectangle;
import flash.text.TextFormatAlign;

import kabam.rotmg.core.model.PlayerModel;
   import kabam.rotmg.game.view.CreditDisplay;
   import kabam.rotmg.ui.view.components.ScreenBase;
   import org.osflash.signals.Signal;
   import org.osflash.signals.natives.NativeMappedSignal;
   
   public class CharacterSelectionAndNewsScreen extends Sprite
   {
      private const DROP_SHADOW:DropShadowFilter = new DropShadowFilter(0,0,0,1,8,8);
      
      private var model:PlayerModel;
      
      private var isInitialized:Boolean;
      
      private var nameText:SimpleText;
      
      private var creditDisplay:CreditDisplay;
      
      private var charactersText:SimpleText;
      
      private var characterList:CharacterList;
      
      private var characterListHeight:Number;
      
      private var playButton:TitleMenuOption;
      
      private var backButton:TitleMenuOption;
      
      private var lines:Shape;

      public var close:Signal;
      
      public var showClasses:Signal;
      
      public var newCharacter:Signal;
      
      public var playGame:Signal;
      
      public function CharacterSelectionAndNewsScreen()
      {
         this.playButton = new TitleMenuOption("play",36,true);
         this.backButton = new TitleMenuOption("back",22,false);
         this.newCharacter = new Signal();
         this.playGame = new Signal();
         super();
         addChild(new ScreenBase());
         addChild(new AccountScreen());
         this.close = new NativeMappedSignal(this.backButton,MouseEvent.CLICK);
          //this.showClasses = new NativeMappedSignal(this.classesButton,MouseEvent.CLICK);
      }
      
      public function initialize(model:PlayerModel) : void
      {
         if(this.isInitialized)
         {
            return;
         }
         this.isInitialized = true;
         this.model = model;
         this.createDisplayAssets(model);
      }
      
      private function createDisplayAssets(model:PlayerModel) : void
      {
         this.createNameText();
         this.createCreditDisplay();
         this.createCharactersText();
         this.createBoundaryLines();
         this.createCharacterList();

         this.createButtons();
         this.positionButtons();
      }
      
      private function createButtons() : void
      {
         addChild(new ScreenGraphic());
         addChild(this.playButton);
         addChild(this.backButton);
         this.playButton.addEventListener(MouseEvent.CLICK,this.onPlayClick);
      }
      
      private function positionButtons() : void
      {
         this.playButton.x = (this.getReferenceRectangle().width - this.playButton.width) / 2;
         this.playButton.y = 520;
         this.backButton.x = (this.getReferenceRectangle().width - this.backButton.width) / 2 - 94;
         this.backButton.y = 532;
      }

      private function createCharacterList() : void
      {
         this.characterList = new CharacterList(this.model);
         this.characterList.x = 10;
         this.characterList.y = 115;
         this.characterListHeight = this.characterList.height;
         addChild(this.characterList);
      }

      private function createCharactersText() : void
      {
         this.charactersText = new SimpleText(18,11776947,false,0,0);
         this.charactersText.setBold(true);
         this.charactersText.text = "Characters";
         this.charactersText.updateMetrics();
         this.charactersText.filters = [this.DROP_SHADOW];
         this.charactersText.setAlignment(TextFormatAlign.LEFT);
         this.charactersText.x = 10;
         this.charactersText.y = 79;
         addChild(this.charactersText);
      }
      
      private function createCreditDisplay() : void
      {
         this.creditDisplay = new CreditDisplay();
         this.creditDisplay.draw(this.model.getCredits());
         this.creditDisplay.x = this.getReferenceRectangle().width;
         this.creditDisplay.y = 20;
         addChild(this.creditDisplay);
      }
      
      private function createNameText() : void
      {
         this.nameText = new SimpleText(22,11776947,false,0,0);
         this.nameText.setBold(true);
         this.nameText.text = this.model.getName() || "Undefined";
         this.nameText.updateMetrics();
         this.nameText.filters = [this.DROP_SHADOW];
         this.nameText.y = 24;
         this.nameText.x = (this.getReferenceRectangle().width - this.nameText.width) / 2;
         addChild(this.nameText);
      }
      
      private function getReferenceRectangle() : Rectangle
      {
         var rectangle:Rectangle = new Rectangle();
         if(stage)
         {
            rectangle = new Rectangle(0,0,stage.stageWidth,stage.stageHeight);
         }
         return rectangle;
      }

      private function createBoundaryLines() : void
      {
         this.lines = new Shape();
         this.lines.graphics.clear();
         this.lines.graphics.lineStyle(2,5526612);
         this.lines.graphics.moveTo(0,105);
         this.lines.graphics.lineTo(this.getReferenceRectangle().width,105);
         this.lines.graphics.lineStyle();
         addChild(this.lines);
      }

      private function onPlayClick(event:Event) : void
      {
         if(this.model.charList.hasCharacter == false){
            this.newCharacter.dispatch();
         }else{
            this.playGame.dispatch();
         }
      }
      
      public function setName(name:String) : void
      {
         this.nameText.text = name;
         this.nameText.updateMetrics();
         this.nameText.x = (this.getReferenceRectangle().width - this.nameText.width) * 0.5;
      }
   }
}
