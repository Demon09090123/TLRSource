package kabam.rotmg.ui.commands {
import kabam.rotmg.account.core.signals.UpdateAccountInfoSignal;
import kabam.rotmg.core.signals.SetScreenSignal;
import kabam.rotmg.ui.welcomeview.WelcomeScreen;

import robotlegs.bender.framework.api.ILogger;

public class WelcomeScreenCommand {

    [Inject]
    public var setScreen:SetScreenSignal;
    [Inject]
    public var updateAccount:UpdateAccountInfoSignal;

    public function WelcomeScreenCommand() {
        super();
    }

    public function execute(): void {
        this.setScreen.dispatch(new WelcomeScreen());
    }
}
}
