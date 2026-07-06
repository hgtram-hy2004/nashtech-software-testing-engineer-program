import { BasePage } from './base-page';
import { Element } from '../core/element/element';

export class PhotographerProfilePage extends BasePage {
    private readonly avatar: Element;
    private readonly photographerName: Element;
    private readonly emailButton: Element;
    private readonly moreActionsButton: Element;
    private readonly introduce: Element;
    private readonly interests: Element;
    private readonly photosList: Element;
    private readonly collectionsList: Element;
    private readonly shareProfileMenuItem: Element;
    private readonly reportButton: Element;

    constructor() {
        super();
        this.avatar = new Element("//img[@role='presentation' and @width='150' and @height='150']");
        this.photographerName = new Element("//div[contains(@class,'name') and contains(@class,'responsiveHeadingL')]");
        this.emailButton = new Element("//button[normalize-space()='Hire']");
        this.moreActionsButton = new Element("//button[@aria-label='More Actions']");
        this.introduce = new Element("//div[contains(@class,'bio')]");
        this.interests = new Element("//div[contains(@class,'interestsListAndEditLink')]");
        this.photosList = new Element("//a[@data-testid='user-nav-link-photos']");
        this.collectionsList = new Element("//a[@data-testid='user-nav-link-collections']");
        this.shareProfileMenuItem = new Element("//div[text()='Share profile']");
        this.reportButton = new Element("//button[text()='Report']");
    }

    async verifyPhotographerProfilePageIsDisplayed(): Promise<void> {
        await this.photographerName.shouldBeVisible();
    }

    async clickMoreActionsButton(): Promise<void> {
        await this.moreActionsButton.shouldBeVisible();
        await this.moreActionsButton.clickOnElement();
    }

    async verifyMoreActionsMenuIsDisplayed(): Promise<void> {
        await this.shareProfileMenuItem.shouldBeVisible();
        await this.reportButton.shouldBeVisible();
    }

    async verifyCommonProfileFieldsAreDisplayed(): Promise<void> {
        await this.avatar.shouldBeVisible();
        await this.photographerName.shouldBeVisible();
        await this.moreActionsButton.shouldBeVisible();

        await this.emailButton.shouldBeVisibleIfExists();
        await this.introduce.shouldBeVisibleIfExists();
        await this.interests.shouldBeVisibleIfExists();
        await this.photosList.shouldBeVisibleIfExists();
        await this.collectionsList.shouldBeVisibleIfExists();
    }
}