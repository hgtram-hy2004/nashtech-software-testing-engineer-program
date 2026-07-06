import { Page } from '@playwright/test';
import { BrowserManagement } from '../../core/browser/browser-management';
import { BrowserUtils } from '../../core/browser/browser-utils';

export class BaseComponent {
    protected get page(): Page {
        return BrowserManagement.getCurrentPage();
    }

    async waitForPageLoad(): Promise<void> {
        await BrowserUtils.waitForPageLoad();
    }

    async waitForTimeout(milliseconds: number): Promise<void> {
        await this.page.waitForTimeout(milliseconds);
    }
}