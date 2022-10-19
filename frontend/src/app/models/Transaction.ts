export class QueryResult {
    filterName: string = "";
    transactions: Transaction[] = [];
}

export class Transaction {
    id: string = "";
    amount: number = 0;
    description: string = "";
    creationTime: Date = new Date();
    category: string = "";
}
