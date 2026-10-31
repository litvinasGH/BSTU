//1
function createPhoneNum(numbers) {
    var areaCode = numbers.slice(0, 3).join('');
    var firstPart = numbers.slice(3, 6).join('');
    var secondPart = numbers.slice(6).join('');
    return "(".concat(areaCode, ") ").concat(firstPart, "-").concat(secondPart);
}
var phoneNum = createPhoneNum([1, 2, 3, 4, 5, 6, 7, 8, 9, 0]);
console.log(phoneNum); // => "(123) 456-7890"
//2
var Challenge = /** @class */ (function () {
    function Challenge() {
    }
    Challenge.solution = function (number) {
        if (number < 0) {
            return 0;
        }
        var sum = 0;
        for (var i = 0; i < number; i++) {
            if (i % 3 === 0 || i % 5 === 0) {
                sum += i;
            }
        }
        return sum;
    };
    return Challenge;
}());
console.log(Challenge.solution(10));
console.log(Challenge.solution(20));
console.log(Challenge.solution(-5));
console.log(Challenge.solution(15));
//3
function rotate(nums, k) {
    var n = nums.length;
    function reverse(nums, start, end) {
        while (start < end) {
            var temp = nums[start];
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
var nums = [1, 2, 3, 4, 5, 6, 7];
rotate(nums, 3);
console.log(nums); // [5, 6, 7, 1, 2, 3, 4]
//4
function findMedianArrays(nums1, nums2) {
    var ret = nums1.concat(nums2);
    ret.sort();
    if (ret.length % 2) {
        return ret[Math.floor(ret.length / 2)];
    }
    else {
        return (ret[ret.length / 2] + ret[ret.length / 2 - 1]) / 2;
    }
}
console.log(findMedianArrays([1, 3], [2]));
console.log(findMedianArrays([1, 2], [3, 4]));
