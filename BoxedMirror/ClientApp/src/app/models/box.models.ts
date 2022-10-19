
interface Room {
    column: number;
    row: number;
    orientation?: string;
}

interface BoxInput {
    columns: number;
    rows: number;
    mirrors: Array<Mirror>;
    entry: Room;
}

interface Mirror {
    column: number;
    row: number;
    leaning: string;
    reflection: string;
}