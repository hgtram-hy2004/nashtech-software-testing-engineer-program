import math


class Shapes(object):
    """
    This class is used as utils to calculate properties of all shapes
    """

    # calculate the area of a rectangle

    #   calculate the perimeter of a rectangle

    #   calculate the width of a rectangle

    #   calculate the length of a rectangle
    @staticmethod
    def calculate_rectangle_length(area, width):
        """Calculates the length of a rectangle given its area and width."""
        if area < 0 or width <= 0:
            raise ValueError("Area must be non-negative and width must be greater than zero")
        return area / width

    #   calculate the area of square

    #   calculate the perimeter of a square

    #   calculate the side of a square

    #   calculate the area of a circle

    #   calculate the radius of a circle
