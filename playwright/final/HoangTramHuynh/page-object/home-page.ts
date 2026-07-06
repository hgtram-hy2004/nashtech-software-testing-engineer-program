import { BasePage } from './base-page';
import { Element } from '../core/element/element';
import { BASE_URL } from '../constant/url';

export class HomePage extends BasePage {
    private readonly loginButton: Element;
    private readonly secondPhoto: Element;

    constructor() {
        super();
        this.loginButton = new Element("//a[text()='Log in']");
        this.secondPhoto = new Element("figure[data-masonryposition='2']:visible");
    }

    async openHomePage(): Promise<void> {
        await this.openUrl(BASE_URL);
    }

    async gotoLogin(): Promise<void> {
        await this.loginButton.shouldBeVisible();
        await this.loginButton.clickOnElement();
        await this.waitForPageLoad();
    }

    async clickSecondPhoto(): Promise<void> {
        await this.secondPhoto.shouldBeVisible();
        await this.secondPhoto.clickOnElement();
        await this.waitForPageLoad();
    }
}