import { BrowserManagement } from "../browser/browser-management";
import { test as baseTest, expect as baseExpect } from '@playwright/test';

export const test = baseTest.extend<{ browserFixture: string }>({
    browserFixture: [async ({ browser, context, page, request }, use) => {
        BrowserManagement.initializeBrowser(browser, context, page, request);
        await use('');
    }, { scope: 'test', auto: true }
    ]
})
export const expect = baseExpect;