import { Page } from '@playwright/test';
import { BrowserManagement } from '../core/browser/browser-management';
import { BrowserUtils } from '../core/browser/browser-utils';
import { SideBarComponent } from './component/side-bar';
import { PhotoCardModalComponent } from './component/photocard-modal';

export class BasePage {
    protected readonly sideBarComponent: SideBarComponent;
    protected readonly photoCardModalComponent: PhotoCardModalComponent;

    constructor() {
        this.sideBarComponent = new SideBarComponent();
        this.photoCardModalComponent = new PhotoCardModalComponent();
    }

    protected get page(): Page {
        return BrowserManagement.getCurrentPage();
    }

    async openUrl(url: string): Promise<void> {
        await BrowserUtils.navigateTo(url);
        await this.waitForPageLoad();
    }

    async waitForPageLoad(): Promise<void> {
        await BrowserUtils.waitForPageLoad();
    }

    async waitForTimeout(milliseconds: number): Promise<void> {
        await this.page.waitForTimeout(milliseconds);
    }

    async hoverPhotographerIcon(): Promise<void> {
        await this.photoCardModalComponent.hoverPhotographerIcon();
    }

    async getPhotographerNameOnHover(): Promise<string> {
        return await this.photoCardModalComponent.getPhotographerNameOnHover();
    }

    async clickViewProfileButton(): Promise<void> {
        await this.photoCardModalComponent.clickViewProfileButton();
    }
}