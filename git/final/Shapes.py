import math


class Shapes(object):
    """
    This class is used as utils to calculate properties of all shapes
    """

    # calculate the area of a rectangle

    # calculate the perimeter of a rectangle

    # calculate the width of a rectangle
    def rectangle_width(area=None, length=None, perimeter=None):
        if area is not None and length is not None:
            return area / length
        elif perimeter is not None and length is not None:
            return (perimeter / 2) - length
        else:
            raise ValueError("Provide (area & length) or (perimeter & length)")
    # calculate the length of a rectangle


    # calculate the area of square

    #   calculate the length of a rectangle
    @staticmethod
    def calculate_rectangle_length(area, width):
        """Calculates the length of a rectangle given its area and width."""
        if area < 0 or width <= 0:
            raise ValueError("Area must be non-negative and width must be greater than zero")
        return area / width



    #   calculate the perimeter of a square
    def perimeterOfSquare(self, side):
        return side * 4

    # calculate the perimeter of a square


    # calculate the side of a square

    # calculate the area of a circle

    # calculate the radius of a circle
