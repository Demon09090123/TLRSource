package kabam.rotmg.ui.view
{
   import com.company.assembleegameclient.screens.AccountScreen;
   import com.company.assembleegameclient.ui.tooltip.ToolTip;
   import kabam.rotmg.account.core.Account;
   import kabam.rotmg.core.model.PlayerModel;
   import kabam.rotmg.core.signals.HideTooltipsSignal;
   import kabam.rotmg.core.signals.ShowTooltipSignal;
   import robotlegs.bender.bundles.mvcs.Mediator;
   
   public class AccountScreenMediator extends Mediator
   {
       
      
      [Inject]
      public var view:AccountScreen;
      
      [Inject]
      public var account:Account;
      
      [Inject]
      public var playerModel:PlayerModel;
      
      [Inject]
      public var showTooltip:ShowTooltipSignal;
      
      [Inject]
      public var hideTooltips:HideTooltipsSignal;
      
      public function AccountScreenMediator()
      {
         super();
      }
      
      override public function initialize() : void
      {
         this.view.tooltip.add(this.onTooltip);
         this.view.setRank(this.playerModel.getLevel());
         this.view.setGuild(this.playerModel.getGuildName(),this.playerModel.getGuildRank());
      }

      
      override public function destroy() : void
      {
         this.view.tooltip.remove(this.onTooltip);
         this.hideTooltips.dispatch();
      }
      
      private function onTooltip(tooltip:ToolTip) : void
      {
         this.showTooltip.dispatch(tooltip);
      }
   }
}
