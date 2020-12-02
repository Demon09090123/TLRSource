package kabam.rotmg.ui.welcomeview {
import com.company.assembleegameclient.screens.TitleMenuOption;
import com.company.ui.SimpleText;

import flash.display.DisplayObject;

import flash.display.Sprite;
import flash.events.MouseEvent;
import flash.filters.DropShadowFilter;
import flash.geom.ColorTransform;

import kabam.rotmg.account.core.view.AccountInfoView;

import org.osflash.signals.Signal;
import org.osflash.signals.natives.NativeMappedSignal;

public class WelcomeScreen extends Sprite implements AccountInfoView {

    private const LOGIN:String = "LOGIN";
    private const LOGOUT:String = "LOGOUT";
    private const REGISTER:String = "REGISTER";
    private const PLAY:String = "PLAY";

    private const WIDTH:int = 800;
    private const HEIGHT:int = 600;

    public var isLoggedIn:Boolean = false;

    private var loginBtn:TitleMenuOption;
    private var registerBtn:TitleMenuOption;
    private var playBtn:TitleMenuOption;

    private var nameText:SimpleText;

    public var loginSignal:Signal;
    public var registerSignal:Signal;
    public var playSignal:Signal;

    public function WelcomeScreen() {

        graphics.beginFill(0x404040);
        graphics.drawRect(0, 0, WIDTH, HEIGHT);
        graphics.endFill();

        this.makeButtons();
        this.makeText("guest", "is guest account");
        this.setupSignal();
    }

    public function setInfo(user:String, isRegistered:Boolean):void {
        trace("regis:" + isRegistered + " | user: " + user);

        while(numChildren) {
            removeChildAt(0);
        }

        if (isRegistered == false) {
            alignDraw(nameText, loginBtn, registerBtn, playBtn);
            return;
        }
        isLoggedIn = true;
        playBtn.activate();
        loginBtn.setText("LOGOUT");
        nameText.setText(user);
        nameText.updateMetrics();

        alignDraw(nameText, loginBtn, playBtn);
    }

    private function alignDraw(... ui):void {
        var yOffset:int = 80;

        for each(var d:DisplayObject in ui) {
            d.x = (WIDTH - d.width) / 2;
            d.y = yOffset;
            yOffset += (d.height + 10);

            addChild(d);
        }
    }

    private function setupSignal():void {
        this.loginSignal = new NativeMappedSignal(this.loginBtn, MouseEvent.CLICK);
        this.registerSignal = new NativeMappedSignal(this.registerBtn, MouseEvent.CLICK);
        this.playSignal = new NativeMappedSignal(this.playBtn, MouseEvent.CLICK);
    }

    private function makeText(name:String, guest:String) :void {
        this.nameText = new SimpleText(18, 0xFFFFFF);
        this.nameText.text = name;
        this.nameText.updateMetrics();
    }

    private function makeButtons():void {

        this.loginBtn = new TitleMenuOption(LOGIN, 28, false, true);
        this.registerBtn = new TitleMenuOption(REGISTER, 28, false, true);
        this.playBtn = new TitleMenuOption(PLAY, 28, false, true);

        this.loginBtn.addOutline(2);
        this.registerBtn.addOutline(2);
        this.playBtn.addOutline(2);

        this.playBtn.deactivate();
    }
}
}
