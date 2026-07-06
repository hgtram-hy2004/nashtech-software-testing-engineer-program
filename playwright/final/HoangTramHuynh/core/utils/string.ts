export class StringUtils {
    static generateRandomUsername(prefix: string): string {
        const dateTime = this.getCurrentDateTime();

        return `${prefix}_${dateTime}`;
    }

    static generateRandomCollectionName(prefix: string): string {
        const dateTime = this.getCurrentDateTime();

        return `${prefix}_${dateTime}`;
    }

    private static getCurrentDateTime(): string {
        return new Date()
            .toISOString()
            .replace(/[-:.TZ]/g, '');
    }
}