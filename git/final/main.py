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
    
    #Rectangle
    width = float(input("Enter the width of the rectangle: "))
    length = float(input("Enter the length of the rectangle: "))
    print(Shapes.rectangle_width(perimeter=perimeter, length=length))

    area = 50
    width = 5
    
    # Task 4: Calculate the length of a rectangle
    length = Shapes.calculate_rectangle_length(area, width)
    print(f"Rectangle with Area={area} and Width={width}:")
    print(f"- Calculated Length: {length}")

if __name__ == "__main__":
    main()
