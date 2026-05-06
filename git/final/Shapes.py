import math


class Shapes(object):
    """
    This class is used as utils to calculate properties of all shapes
    """

    # Task 1. Tram : calculate the area of a rectangle
    def rectangle_area(length, width):
        if not isinstance(length, (int, float)) or not isinstance(width, (int, float)):
            raise TypeError("Length and width must be numbers")
        return length * width

    # calculate the perimeter of a rectangle

    # Task 2. Le Tan Dat : calculate the perimeter of a rectangle
    @staticmethod
    def calculate_rectangle_perimeter(length, width):
        """Calculates the perimeter of a rectangle given its length and width."""
        if length <= 0 or width <= 0:
            raise ValueError("Length and width must be greater than zero")
        return 2 * (length + width)

    # Task 3. Bao : calculate the width of a rectangle
    @staticmethod
    def rectangle_width(area=None, length=None, perimeter=None):
        if area is not None and length is not None:
            return area / length
        elif perimeter is not None and length is not None:
            return (perimeter / 2) - length
        else:
            raise ValueError("Provide (area & length) or (perimeter & length)")

    #  Task 4. Khang. calculate the length of a rectangle
    @staticmethod
    def calculate_rectangle_length(area, width):
        """Calculates the length of a rectangle given its area and width."""
        if area < 0 or width <= 0:
            raise ValueError(
                "Area must be non-negative and width must be greater than zero"
            )
        return area / width

    # Task 5. Manh: calculate the area of square
    @staticmethod
    def calculate_square_area(side):
        return side * side

    #  Task 6. Đào:  calculate the perimeter of a square
    def perimeterOfSquare(self, side):
        return side * 4

    # Task 7. Hằng : calculate_square_side(area)
    def calculate_square_side(area=None, perimeter=None):
        """
        Calculate the side of a square.
        - If area is provided → side = sqrt(area)
        - If perimeter is provided → side = perimeter / 4
        """
        if area is not None:
            return math.sqrt(area)
        elif perimeter is not None:
            return perimeter / 4
        else:
            raise ValueError("Please provide either area or perimeter")

    # Task 8. Manh: calculate the area of a circle
    @staticmethod
    def calculate_circle_area(radius):
        return math.pi * radius * radius
