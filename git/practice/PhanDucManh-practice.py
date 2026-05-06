def calculate_sum(numbers):
    total = 0

    for number in numbers:
        total += number

    return total


def find_max(numbers):
    if len(numbers) == 0:
        return None

    max_number = numbers[0]

    for number in numbers:
        if number > max_number:
            max_number = number

    return max_number


def is_palindrome(text):
    normalized_text = text.lower()

    return normalized_text == normalized_text[::-1]


print(calculate_sum([1, 2, 3, 4, 5]))
print(find_max([10, 4, 25, 8]))
print(is_palindrome("level"))
