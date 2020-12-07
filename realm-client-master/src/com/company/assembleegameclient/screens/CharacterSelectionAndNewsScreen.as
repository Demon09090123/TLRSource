package com.company.assembleegameclient.screens
{

import drawing.components.TitleMenuOption;

import drawing.screen.DarkLayer;
import drawing.uiAssets.UIComponentHelper;
   import flash.display.Sprite;
   import flash.events.Event;
   import flash.events.MouseEvent;
   import flash.geom.Rectangle;

import kabam.rotmg.core.model.PlayerModel;

import org.osflash.signals.Signal;
   import org.osflash.signals.natives.NativeMappedSignal;
   
   public class CharacterSelectionAndNewsScreen extends Sprite
   {
      private var isInitialized:Boolean;
      private var characterListHeight:Number;

      private var characterList:CharacterList;
      private var model:PlayerModel;
      
      //private var playButton:TitleMenuOption;
      //private var backButton:TitleMenuOption;

      public var close:Signal;
      public var newCharacter:Signal;
      public var playGame:Signal;
      
      public function CharacterSelectionAndNewsScreen()
      {
         //this.playButton = new TitleMenuOption("play",36,true);
         //this.backButton = new TitleMenuOption("back",22,false);

         this.newCharacter = new Signal();
         this.playGame = new Signal();
         super();

         addChild(UIComponentHelper._charScreen);
         addChild(new DarkLayer());

         //this.close = new NativeMappedSignal(this.backButton,MouseEvent.CLICK);
      }
      
      public function initialize(model:PlayerModel) : void
      {
         if(this.isInitialized)
         {
            return;
         }
         this.isInitialized = true;
         this.model = model;
         this.createDisplayAssets();
      }
      
      private function createDisplayAssets() : void
      {
         this.createCharacterList();

         this.createButtons();
         this.positionButtons();
      }
      
      private function createButtons() : void
      {
         //addChild(this.playButton);
         //addChild(this.backButton);
         //this.playButton.addEventListener(MouseEvent.CLICK,this.onPlayClick);
      }
      
      private function positionButtons() : void
      {
         //this.playButton.x = (this.getReferenceRectangle().width - this.playButton.width) / 2;
         //this.playButton.y = 520;
         //this.backButton.x = (this.getReferenceRectangle().width - this.backButton.width) / 2 - 94;
         //this.backButton.y = 532;
      }

      private function createCharacterList() : void
      {
         this.characterList = new CharacterList(this.model);
         this.characterListHeight = this.characterList.height;
         addChild(this.characterList);
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
      private function onPlayClick(event:Event) : void
      {
         if(this.model.hasCharacter()){
            this.newCharacter.dispatch();
         }else{
            this.playGame.dispatch();
         }
      }
      
      public function setName(name:String) : void
      {
         //this..setName(name);
      }
   }
}
