//1
function createPhoneNum(numbers: number[]): string {
    const areaCode = numbers.slice(0, 3).join('');
    const firstPart = numbers.slice(3, 6).join('');
    const secondPart = numbers.slice(6).join('');

    return `(${areaCode}) ${firstPart}-${secondPart}`;
}

const phoneNum = createPhoneNum([1, 2, 3, 4, 5, 6, 7, 8, 9, 0]);
console.log(phoneNum); // => "(123) 456-7890"

//2
class Challenge {
    static solution(number: number): number {
        if (number < 0) {
            return 0;
        }

        let sum = 0;

        for (let i = 0; i < number; i++) {
            if (i % 3 === 0 || i % 5 === 0) {
                sum += i;
            }
        }

        return sum;
    }
}

console.log(Challenge.solution(10)); 
console.log(Challenge.solution(20)); 
console.log(Challenge.solution(-5));
console.log(Challenge.solution(15));


//3

function rotate(nums: number[], k: number): void {
    const n = nums.length;

    function reverse(nums: number[], start: number, end: number): void {
        while (start < end) {
            const temp = nums[start];
            nums[start] = nums[end];
            nums[end] = temp;
            start++;
            end--;
        }
    }
    
    reverse(nums, 0, n - 1);
    reverse(nums, 0, k - 1);
    reverse(nums, k, n - 1);
}




const nums = [1, 2, 3, 4, 5, 6, 7];
rotate(nums, 3);
console.log(nums); // [5, 6, 7, 1, 2, 3, 4]