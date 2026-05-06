from Shapes import Shapes


def main():

    # 2. Le Tan Dat: Calculate Rectangle Perimeter
    try:
        width = float(input("Enter the width of the rectangle: "))
        length = float(input("Enter the length of the rectangle: "))

        perimeter = Shapes.calculate_rectangle_perimeter(length, width)

        print(f"The perimeter of the rectangle is: {perimeter}")
    except ValueError as error:
        print(f"Invalid rectangle perimeter input: {error}")

    # Rectangle
    width = float(input("Enter the width of the rectangle: "))
    length = float(input("Enter the length of the rectangle: "))
    print(Shapes.rectangle_width(perimeter=perimeter, length=length))

    # 1. Area of The Rectangle
    length_area = 10
    print("Area of ​​rectangle: ", Shapes.rectangle_area(length_area, width))
    # 6. Perimeter of The Square
    shapes = Shapes()
    print("The perimeter of the square is:", shapes.perimeterOfSquare(8))

    # 3. Rectangle
    print("The width of the rectangle is: ", Shapes.rectangle_width(area=50, length=10))

    # Task 4: Calculate the length of a rectangle

    # global variable
    area = 50
    width = 5
    length = Shapes.calculate_rectangle_length(area, width)
    print(f"Rectangle with Area={area} and Width={width}:")
    print(f"- Calculated Length: {length}")

    # Task 5
    square_side = float(input("Enter square side: "))
    square_area = Shapes.calculate_square_area(square_side)
    print("Square area:", square_area)

    # 7. Square side
    print("Side from area:", Shapes.calculate_square_side(area=64))  # 8.0
    print("Side from perimeter:", Shapes.calculate_square_side(perimeter=20))  # 5.0

    # 8. Circle area
    circle_radius = float(input("Enter circle radius: "))
    circle_area = Shapes.calculate_circle_area(circle_radius)
    print("Circle area:", circle_area)


if __name__ == "__main__":

    main()
