import math


class Shapes(object):
    """
    This class is used as utils to calculate properties of all shapes
    """

    # calculate the area of a rectangle

    #   calculate the perimeter of a rectangle

    #   calculate the width of a rectangle

    #   calculate the length of a rectangle

    #   calculate the area of square

    #   calculate the perimeter of a square

    #   calculate the side of a square

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


    # if __name__ == "__main__":
        # Test cases
        
    
    #   calculate the area of a circle

    #   calculate the radius of a circle
