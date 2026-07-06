import { BasePage } from './base-page';
import { Element } from '../core/element/element';
import { BASE_URL } from '../constant/url';
import { AccountData } from '../data-object/ui/account';

export class LoginPage extends BasePage {
    private readonly emailInput: Element;
    private readonly passwordInput: Element;
    private readonly loginButton: Element;

    constructor() {
        super();
        this.emailInput = new Element("//input[@name='email']");
        this.passwordInput = new Element("//input[@name='password']");
        this.loginButton = new Element("//button[@type='submit' and @value='Login']");
    }

    async openLoginPage(): Promise<void> {
        await this.openUrl(`${BASE_URL}/login`);
    }

    async enterEmail(email: string): Promise<void> {
        await this.emailInput.enterText(email);
    }

    async enterPassword(password: string): Promise<void> {
        await this.passwordInput.enterText(password);
    }

    async clickLoginButton(): Promise<void> {
        await this.loginButton.clickOnElement();
    }

    async login(accountInfo: AccountData): Promise<void> {
        await this.enterEmail(accountInfo.email);
        await this.enterPassword(accountInfo.password);
        await this.clickLoginButton();
        await this.waitForPageLoad();
        await this.waitForTimeout(6000);
    }
}