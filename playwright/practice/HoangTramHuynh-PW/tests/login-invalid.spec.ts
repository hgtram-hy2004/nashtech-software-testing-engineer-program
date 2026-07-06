import { test, expect } from '../core/fixture/base-fixture';
import loginData from '../test-data/login-data.json';

test.describe('Login Function', () => {
  for (const account of loginData.invalidAccounts) {
    test(`Verify that the error message is displayed when ${account.caseName}`, async ({
      loginPage,
    }) => {
      await test.step('Given the user visits the TMS website', async () => {
        await loginPage.openLoginPage();
      });

      await test.step('When the user inputs an invalid account and clicks Login button', async () => {
        await loginPage.login(account.username, account.password);
      });

        await test.step('Then the error message will be displayed', async () => {
        await loginPage.verifyLoginFailedMessageIsDisplayed();
      });
    });
  }
});