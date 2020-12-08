package com.company.assembleegameclient.ui
{
   import com.company.assembleegameclient.objects.Player;
   import com.company.assembleegameclient.ui.tooltip.TextToolTip;
   import flash.display.Sprite;
   import flash.events.Event;
   import flash.events.MouseEvent;

import kabam.rotmg.messaging.impl.data.StatData;

public class Stats extends Sprite
   {
      
      public static const ATTACK:int = 0;
      public static const MAGIC_POWER:int = 1;
      public static const PHYSICAL_DEFENSE:int = 2;
      public static const MAGIC_DEFENSE:int = 3;
      public static const SPEED:int = 4;
      public static const DEXTERITY:int = 5;
      public static const VITALITY:int = 6;
      public static const WISDOM:int = 7;
      
      public static const statsXML:XML = <Stats>
                <Stat>
                    <Abbr>ATT</Abbr>
                    <Name>Attack</Name>
                    <Description>This stat increases the amount of damage done.</Description>
                    <RedOnZero/>
                </Stat>
                <Stat>
                    <Abbr>MGP</Abbr>
                    <Name>Magic Attack</Name>
                    <Description>This stat increases the amount of magic damage done.</Description>
                    <RedOnZero/>
                </Stat>
                <Stat>
                    <Abbr>PDEF</Abbr>
                    <Name>Physical Defense</Name>
                    <Description>This stat decreases the amount of physical damage taken.</Description>
                </Stat>
                <Stat>
                    <Abbr>MDEF</Abbr>
                    <Name>Magic Defense</Name>
                    <Description>This stat decreases the amount of magic damage taken.</Description>
                </Stat>
                <Stat>
                    <Abbr>SPD</Abbr>
                    <Name>Speed</Name>
                    <Description>This stat increases the speed at which the character moves.</Description>
                    <RedOnZero/>
                </Stat>
                <Stat>
                    <Abbr>DEX</Abbr>
                    <Name>Dexterity</Name>
                    <Description>This stat increases the speed at which the character attacks.</Description>
                    <RedOnZero/>
                </Stat>
                <Stat>
                    <Abbr>VIT</Abbr>
                    <Name>Vitality</Name>
                    <Description>This stat increases the speed at which hit points are recovered.</Description>
                    <RedOnZero/>
                </Stat>
                <Stat>
                    <Abbr>WIS</Abbr>
                    <Name>Wisdom</Name>
                    <Description>This stat increases the speed at which magic points are recovered.</Description>
                    <RedOnZero/>
                </Stat>
            </Stats>;
       
      
      public var w_:int;
      
      public var h_:int;
      
      public var stats_:Vector.<Stat>;
      
      public var toolTip_:TextToolTip;
      
      public function Stats(w:int, h:int)
      {
         var statXML:XML = null;
         var stat:Stat = null;
         this.stats_ = new Vector.<Stat>();
         this.toolTip_ = new TextToolTip(3552822,10197915,"","",200);
         super();
         this.w_ = w;
         this.h_ = h;
         for each(statXML in statsXML.Stat)
         {
            stat = new Stat(statXML.Abbr,statXML.Name,statXML.Description,statXML.hasOwnProperty("RedOnZero"));
            addChild(stat);
            this.stats_.push(stat);
         }
         addEventListener(Event.ADDED_TO_STAGE,this.onAddedToStage);
         addEventListener(Event.REMOVED_FROM_STAGE,this.onRemovedFromStage);
      }

      private function reposition():void {
          var oWidth:int = 0;

          for each(var s1:Stat in this.stats_) {
              if (s1.width + 5 > oWidth) {
                  oWidth = s1.width + 5;
              }
          }

          var yOffset:int = 0;
          var xOffset:int = 0;

          for each(var s:Stat in this.stats_) {
              s.x = xOffset;
              s.y = yOffset;

              xOffset += oWidth;
              if (xOffset > this.w_) {
                  xOffset = 0;
                  yOffset += s.height + 5;
              }
          }
      }
      
      public function draw(go:Player) : void
      {
         this.stats_[ATTACK].draw(go.attack_);
         this.stats_[MAGIC_POWER].draw(go.magicPower_)
         this.stats_[PHYSICAL_DEFENSE].draw(go.physicalDefense);
         this.stats_[MAGIC_DEFENSE].draw(go.magicDefense_);
         this.stats_[SPEED].draw(go.speed_);
         this.stats_[DEXTERITY].draw(go.dexterity_);
         this.stats_[VITALITY].draw(go.vitality_);
         this.stats_[WISDOM].draw(go.wisdom_);

         reposition();
      }

      public function drawXML(stats:XML):void {
          this.stats_[ATTACK].draw(int(stats.Attack));
          this.stats_[MAGIC_POWER].draw(int(stats.MagicPower))
          this.stats_[PHYSICAL_DEFENSE].draw(int(stats.PhysicalDefense));
          this.stats_[MAGIC_DEFENSE].draw(int(stats.MagicDefense));
          this.stats_[SPEED].draw(int(stats.Speed));
          this.stats_[DEXTERITY].draw(int(stats.Dexterity));
          this.stats_[VITALITY].draw(int(stats.HpRegen));
          this.stats_[WISDOM].draw(int(stats.MpRegen));

          reposition();
      }
      
      private function onAddedToStage(event:Event) : void
      {
         var stat:Stat = null;
         for each(stat in this.stats_)
         {
            stat.addEventListener(MouseEvent.MOUSE_OVER,this.onMouseOver);
            stat.addEventListener(MouseEvent.MOUSE_OUT,this.onMouseOut);
         }
      }
      
      private function onRemovedFromStage(event:Event) : void
      {
         var stat:Stat = null;
         if(this.toolTip_.parent != null)
         {
            this.toolTip_.parent.removeChild(this.toolTip_);
         }
         for each(stat in this.stats_)
         {
            stat.addEventListener(MouseEvent.MOUSE_OVER,this.onMouseOver);
            stat.addEventListener(MouseEvent.MOUSE_OUT,this.onMouseOut);
         }
      }
      
      private function onMouseOver(event:MouseEvent) : void
      {
         var stat:Stat = event.target as Stat;
         this.toolTip_.setTitle(stat.fullName_);
         this.toolTip_.setText(stat.description_);
         if(!stage.contains(this.toolTip_))
         {
            stage.addChild(this.toolTip_);
         }
      }
      
      private function onMouseOut(event:MouseEvent) : void
      {
         var stat:Stat = event.target as Stat;
         if(this.toolTip_.parent != null)
         {
            this.toolTip_.parent.removeChild(this.toolTip_);
         }
      }
   }
}

import com.company.ui.SimpleText;
import flash.display.Sprite;
import flash.filters.DropShadowFilter;
import flash.text.TextFormat;

class Stat extends Sprite
{
    
   
   public var fullName_:String;
   
   public var description_:String;
   
   public var nameText_:SimpleText;
   
   public var valText_:SimpleText;
   
   public var redOnZero_:Boolean;
   
   public var val_:int = -1;
   
   public var valColor_:uint = 11776947;
   
   function Stat(name:String, fullName:String, desc:String, redOnZero:Boolean)
   {
      super();
      this.fullName_ = fullName;
      this.description_ = desc;

      this.nameText_ = new SimpleText(12,11776947,false,0,0);
      this.nameText_.text = name + " -";
      this.nameText_.updateMetrics();
      this.nameText_.x = 0;
      addChild(this.nameText_);

      this.valText_ = new SimpleText(12,this.valColor_,false,0,0);
      this.valText_.setBold(true);
      this.valText_.text = "-";
      this.valText_.updateMetrics();
      this.valText_.x = this.nameText_.width;

      addChild(this.valText_);

      this.redOnZero_ = redOnZero;
   }
   
   public function draw(val:int) : void
   {
      var newValColor:uint = 0;
      var format:TextFormat = null;
      if(val == this.val_)
      {
         return;
      }
      this.val_ = val;
       if(this.redOnZero_ && this.val_ <= 0)
      {
         newValColor = 16726072;
      }
      else
      {
         newValColor = 11776947;
      }
      if(this.valColor_ != newValColor)
      {
         this.valColor_ = newValColor;
         format = this.valText_.defaultTextFormat;
         format.color = this.valColor_;
         this.valText_.setTextFormat(format);
         this.valText_.defaultTextFormat = format;
      }
      this.valText_.text = this.val_.toString();
      this.valText_.updateMetrics();
   }
}
