import fs from 'fs';
import path from 'path';
import { parse } from 'csv-parse/sync';

export class CSVHelper {
    static readCSVFile(fileName: string): any[] {
        const filePath = path.join(path.resolve(__dirname, '../../'), fileName);

        const records = parse(fs.readFileSync(filePath), {
            columns: true,
            skip_empty_lines: true,
        });

        return records;
    }
}