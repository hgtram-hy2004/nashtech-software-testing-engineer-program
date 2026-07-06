import { test } from '../../fixture/page-fixture';
import { CSVHelper } from '../../core/utils/csv';
import { AccountData } from '../../data-object/ui/account';

const accountInfos: AccountData[] = CSVHelper.readCSVFile('test-data/account-data.csv');

for (const accountInfo of accountInfos) {
    test(`Verify view photographer profile successfully with account ${accountInfo.accountName}`, async ({ photographerWorkflow,
    }) => {
        await photographerWorkflow.login(accountInfo);
        await photographerWorkflow.openSecondPhotoDetail();
        await photographerWorkflow.goToPhotographerProfileFromPhotoDetail();

        await photographerWorkflow.verifyPhotographerProfilePageIsDisplayed();
        await photographerWorkflow.verifyMoreActionsMenuIsDisplayed();
        await photographerWorkflow.verifyCommonProfileFieldsAreDisplayed();
    });
}