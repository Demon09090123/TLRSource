package com.company.assembleegameclient.ui.tooltip
{
   import com.company.assembleegameclient.objects.ObjectLibrary;
   import com.company.assembleegameclient.objects.Player;
   import com.company.assembleegameclient.ui.GameObjectListItem;
   import com.company.assembleegameclient.ui.StatusBar;
   import com.company.assembleegameclient.ui.panels.itemgrids.EquippedGrid;
   import com.company.assembleegameclient.ui.panels.itemgrids.InventoryGrid;
   import com.company.ui.SimpleText;

import drawing.components.LineBreakDesign;

import flash.filters.DropShadowFilter;
   import kabam.rotmg.assets.services.CharacterFactory;
   import kabam.rotmg.classes.model.CharacterClass;
   import kabam.rotmg.classes.model.CharacterSkin;
   import kabam.rotmg.classes.model.ClassesModel;
   import kabam.rotmg.constants.GeneralConstants;
   import kabam.rotmg.core.StaticInjectorContext;
   
   public class MyPlayerToolTip extends ToolTip
   {
       
      
      private var factory:CharacterFactory;
      
      private var classes:ClassesModel;
      
      public var player_:Player;
      
      private var playerPanel_:GameObjectListItem;
      
      private var hpBar_:StatusBar;
      
      private var mpBar_:StatusBar;
      
      private var lineBreak_:LineBreakDesign;
      
      private var bestLevel_:SimpleText;
      
      private var nextClassQuest_:SimpleText;
      
      private var eGrid:EquippedGrid;
      
      private var iGrid:InventoryGrid;
      
      public function MyPlayerToolTip(accountName:String, charXML:XML, lvl:int)
      {
         super(3552822,1,16777215,1);
         this.factory = StaticInjectorContext.getInjector().getInstance(CharacterFactory);
         this.classes = StaticInjectorContext.getInjector().getInstance(ClassesModel);
         var objectType:int = int(charXML.ObjectType);
         var playerXML:XML = ObjectLibrary.xmlLibrary_[objectType];
         this.player_ = Player.fromPlayerXML(accountName,charXML);
         var char:CharacterClass = this.classes.getCharacterClass(this.player_.objectType_);
         var skin:CharacterSkin = char.skins.getSkin(charXML.Texture);
         this.player_.animatedChar_ = this.factory.makeCharacter(skin.template);
         this.playerPanel_ = new GameObjectListItem(11776947,true,this.player_);
         addChild(this.playerPanel_);
         this.hpBar_ = new StatusBar(176,16,14693428,5526612,164777215, false,"HP");
         this.hpBar_.x = 6;
         this.hpBar_.y = 40;
         addChild(this.hpBar_);
         this.mpBar_ = new StatusBar(176,16,6325472,5526612, 164777215, false, "MP");
         this.mpBar_.x = 6;
         this.mpBar_.y = 64;
         addChild(this.mpBar_);
         this.eGrid = new EquippedGrid(null,this.player_.slotTypes_,this.player_);
         this.eGrid.x = 8;
         this.eGrid.y = 88;
         addChild(this.eGrid);
         this.eGrid.setItems(this.player_.equipment_, this.player_.itemDatas_);
         this.iGrid = new InventoryGrid(null,this.player_,GeneralConstants.NUM_EQUIPMENT_SLOTS);
         this.iGrid.x = 8;
         this.iGrid.y = 132;
         addChild(this.iGrid);
         this.iGrid.setItems(this.player_.equipment_, this.player_.itemDatas_);
         this.lineBreak_ = new LineBreakDesign(100,1842204);
         this.lineBreak_.x = 6;
         this.lineBreak_.y = 228;
         addChild(this.lineBreak_);
         this.bestLevel_ = new SimpleText(14,6206769,false,0,0);
         this.bestLevel_.text =  "Level: " + lvl;
         this.bestLevel_.updateMetrics();
         this.bestLevel_.filters = [new DropShadowFilter(0,0,0)];
         this.bestLevel_.x = 8;
         this.bestLevel_.y = height - 2;
         addChild(this.bestLevel_);
      }
      
      override public function draw() : void
      {
         this.hpBar_.draw(this.player_.hp_,this.player_.maxHP_);
         this.mpBar_.draw(this.player_.mp_,this.player_.maxMP_);
         this.lineBreak_.setWidthColor(width - 10,1842204);
         super.draw();
      }
   }
}
