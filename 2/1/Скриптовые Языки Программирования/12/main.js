class Sudoku {
    constructor() {
        this.field = document.querySelector('.sudoku-field');
        this.board = Array.from({ length: 9 }, () => Array(9).fill(0));
        this.cellInputs = [];
        this.generateField();
    }

    generateField() {
        this.field.innerHTML = '';
        this.cellInputs = [];

        for (let row = 0; row < 9; row++) {
            for (let col = 0; col < 9; col++) {
                const cell = document.createElement('input');
                cell.type = 'number';
                cell.min = '1';
                cell.max = '9';
                cell.style.width = '50px';
                cell.style.height = '50px';
                cell.style.textAlign = 'center';

                

                cell.style.borderBottom = row === 2 || row === 5 ? '3px solid black' : '';
                cell.style.borderRight = col === 2 || col === 5 ? '3px solid black' : '';
                cell.style.borderTop = row === 3 || row === 6 ? '3px solid black' : '';
                cell.style.borderLeft = col === 3 || col === 6 ? '3px solid black' : '';

                cell.addEventListener('input', (e) => {
                    const value = parseInt(e.target.value, 10);
                    this.board[row][col] = value >= 1 && value <= 9 ? value : 0;
                    e.target.value = value >= 1 && value <= 9 ? value : null;
                    cell.style.backgroundColor = '';
                });

                this.cellInputs.push(cell);
                this.field.appendChild(cell);
            }
            this.field.appendChild(document.createElement('br'));
        }
    }

    resetField() {
        this.board = Array.from({ length: 9 }, () => Array(9).fill(0));
        this.cellInputs.forEach(cell => {
            cell.value = '';
            cell.style.backgroundColor = '';
        });
        let num = 0;
        let start = 0;
        let flag1 = false;
        let flag2 = false;
        for (let i = 0; i < 9; i++) {
            for(let j = 0; j < 9; j++){
                this.board[i][j] = ++num;
                if(num > 8){
                    num = 0;
                }
            }
            num = start + ((i + 1) % 3) * 3;
            if (i === 2 && !flag1){
                num = 1;
                start = 1;
                flag1 = true;
            }
            if(i === 5 && !flag2){
                num = 2;
                start = 2;
                flag2 = true;
            }
        }
        for (let i = 0; i < (9*9)-5; i++) {
            let xcord = Math.floor(Math.random() * 9);
            let ycord = Math.floor(Math.random() * 9);
            if (this.board[xcord][ycord] != 0){
                this.board[xcord][ycord] = 0
            }
            else{
                i--
            }
        }
        

        this.board.forEach((row, rowIndex) => {
            row.forEach((value, colIndex) => {
                const cell = this.cellInputs[rowIndex * 9 + colIndex];
                cell.value = value;
                if (value == 0){
                    cell.value = null;
                }
            });
        });
    }

    validateField() {
        console.group("Test")
        this.cellInputs.forEach(cell => (cell.style.backgroundColor = ''));
        const hasDuplicates = array => {
            const numbers = array.filter(num => num !== 0 && num !== null);
            return new Set(numbers).size !== numbers.length;
        };
        for (let i = 0; i < 9; i++) {
            if (hasDuplicates(this.board[i])) {
                this.highlightRow(i);
            }
            const column = this.board.map(row => row[i]);
            if (hasDuplicates(column)) {
                this.highlightColumn(i);
            }
        }
        for (let sr = 0; sr < 3; sr++) {
            for (let sc = 0; sc < 3; sc++) {
                const square = [];
                for (let r = sr * 3; r < sr * 3 + 3; r++) {
                    for (let c = sc * 3; c < sc * 3 + 3; c++) {
                        square.push(this.board[r][c]);
                    }
                }
                if (hasDuplicates(square)) {
                    this.highlightSquare(sr, sc);
                }
            }
        }
        console.groupEnd();
    }

    highlightRow(row) {
        console.log("row: ", row+1)
        for (let col = 0; col < 9; col++) {
            this.cellInputs[row * 9 + col].style.backgroundColor = 'red';
        }
    }

    highlightColumn(col) {
        console.log("col: ", col+1)
        for (let row = 0; row < 9; row++) {
            this.cellInputs[row * 9 + col].style.backgroundColor = 'red';
        }
    }

    highlightSquare(sr, sc) {
        console.log("square: ", sr+1, ", ", sc+1)
        for (let r = sr * 3; r < sr * 3 + 3; r++) {
            for (let c = sc * 3; c < sc * 3 + 3; c++) {
                this.cellInputs[r * 9 + c].style.backgroundColor = 'red';
            }
        }
    }

    generateValidBoard() {
        

        this.resetField();

        let num = 0;
        let start = 0;
        let flag1 = false;
        let flag2 = false;
        for (let i = 0; i < 9; i++) {
            for(let j = 0; j < 9; j++){
                this.board[i][j] = ++num;
                if(num > 8){
                    num = 0;
                }
            }
            num = start + ((i + 1) % 3) * 3;
            if (i === 2 && !flag1){
                num = 1;
                start = 1;
                flag1 = true;
            }
            if(i === 5 && !flag2){
                num = 2;
                start = 2;
                flag2 = true;
            }
        }

        

        this.board.forEach((row, rowIndex) => {
            row.forEach((value, colIndex) => {
                const cell = this.cellInputs[rowIndex * 9 + colIndex];
                cell.value = value;
            });
        });
    }
    show(){
        console.table(this.board);
    }
    Add(row, col, value){
        this.board[row][col] = value >= 1 && value <= 9 ? value : null;
        this.cellInputs[row*9 + col].value = value >= 1 && value <= 9 ? value : null;
    }
}

const sudoku = new Sudoku();
document.getElementById('generate-empty').addEventListener('click', () => sudoku.resetField());
document.getElementById('validate-field').addEventListener('click', () => sudoku.validateField());
document.getElementById('generate-valid-board').addEventListener('click', () => sudoku.generateValidBoard());