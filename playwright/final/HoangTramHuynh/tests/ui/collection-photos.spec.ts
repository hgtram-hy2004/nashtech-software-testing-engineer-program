import { test } from '../../fixture/page-fixture';
import { CSVHelper } from '../../core/utils/csv';
import { StringUtils } from '../../core/utils/string';
import { AccountData } from '../../data-object/ui/account';

const accountInfos: AccountData[] = CSVHelper.readCSVFile('test-data/account-data.csv');

for (const accountInfo of accountInfos) {
    test(`Verify list of photos in collection successfully with account ${accountInfo.accountName}`, async ({ collectionWorkflow,
    }) => {
        const collectionName = StringUtils.generateRandomCollectionName('tram_collection');
        let collectionId = '';
        await collectionWorkflow.login(accountInfo);

        collectionId = await collectionWorkflow.createCollectionWithThreeRandomPhotos(collectionName);
        await collectionWorkflow.goToUserCollectionsPage(accountInfo.username);

        await collectionWorkflow.verifyCollectionImageCount(collectionName);
        await collectionWorkflow.verifyThreePhotosAppearInCollection(collectionName);

        await collectionWorkflow.deleteCollectionByAPI(collectionId);
    });
}