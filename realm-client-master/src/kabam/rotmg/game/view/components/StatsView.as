package kabam.rotmg.game.view.components
{
   import com.company.assembleegameclient.objects.Player;
import com.company.assembleegameclient.ui.Stats;

   import flash.display.Sprite;
   import kabam.rotmg.game.model.StatModel;


public class StatsView extends Sprite
   {

      private static const statsModel:Array = [new StatModel("ATT","Attack","This stat increases the amount of damage done.",true),new StatModel("DEF","Defense","This stat decreases the amount of damage taken.",false),new StatModel("SPD","Speed","This stat increases the speed at which the character moves.",true),new StatModel("DEX","Dexterity","This stat increases the speed at which the character attacks.",true),new StatModel("VIT","Vitality","This stat increases the speed at which hit points are recovered.",true),new StatModel("WIS","Wisdom","This stat increases the speed at which magic points are recovered.",true)];
       
      
      public var w_:int;
      public var h_:int;
      public var stats_:Vector.<StatView>;
      public var containerSprite:Sprite;

      public function StatsView(w:int, h:int)
      {
         var i:int = 0;
         var statModel:StatModel = null;
         var stat:StatView = null;
         this.stats_ = new Vector.<StatView>();
         this.containerSprite = new Sprite();
         super();
         this.w_ = w;
         this.h_ = h;
         var rows:int = 0;
         for(i = 0; i < statsModel.length; i++)
         {
            statModel = statsModel[i];
            stat = new StatView(statModel.name,statModel.abbreviation,statModel.description,statModel.redOnZero);
            stat.x = i % 2 * this.w_ / 2;
            stat.y = rows * (this.h_ / 3);
            this.containerSprite.addChild(stat);
            this.stats_.push(stat);
            rows = rows + i % 2;
         }
         addChild(this.containerSprite);
      }

      public function draw(go:Player) : void
      {
         if(go != null)
         {
            this.stats_[Stats.ATTACK].draw(go.attack_);
            this.stats_[Stats.MAGIC_POWER].draw(go.magicPower_);
            this.stats_[Stats.PHYSICAL_DEFENSE].draw(go.physicalDefense);
            this.stats_[Stats.MAGIC_DEFENSE].draw(go.magicDefense_);
            this.stats_[Stats.SPEED].draw(go.speed_);
            this.stats_[Stats.DEXTERITY].draw(go.dexterity_);
            this.stats_[Stats.VITALITY].draw(go.vitality_);
            this.stats_[Stats.WISDOM].draw(go.wisdom_);
         }
         this.containerSprite.x = 30 + (191 - this.containerSprite.width) * 0.5;
      }
   }
}
