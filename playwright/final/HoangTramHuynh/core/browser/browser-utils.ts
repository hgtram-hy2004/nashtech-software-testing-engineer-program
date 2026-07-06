import { Page } from '@playwright/test';
import { BrowserManagement } from './browser-management';

export class BrowserUtils {
    static getPage(): Page {
        return BrowserManagement.getCurrentPage();
    }

    static async navigateTo(url: string): Promise<void> {
        await this.getPage().goto(url);
    }

    static async waitForPageLoad(): Promise<void> {
        await this.getPage().waitForLoadState('domcontentloaded');
    }

    static async waitForTimeout(timeout: number = 1000): Promise<void> {
        await this.getPage().waitForTimeout(timeout);
    }

    static async takeScreenshot(fileName: string): Promise<void> {
        await this.getPage().screenshot({
            path: `test-results/screenshots/${fileName}.png`,
            fullPage: true,
        });
    }
}