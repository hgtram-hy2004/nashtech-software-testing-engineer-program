from Shapes import Shapes

def main():

    
    

    #global variable
    area = 50
    width = 5
    #1. Area of The Rectangle
    length = 10
    print("The area of the rectangle: ", Shapes.rectangle_area(length, width))
    #6. Perimeter of The Square 
    shapes = Shapes()
    print("The perimeter of the square is:", shapes.perimeterOfSquare(8))


    #3. Rectangle
    width = float(input("Enter the width of the rectangle: "))
    length = float(input("Enter the length of the rectangle: "))
    print(Shapes.rectangle_width(area=area, length=length, parameter=perimeter))

    
    
    # Task 4: Calculate the length of a rectangle
    length = Shapes.calculate_rectangle_length(area, width)
    print(f"Rectangle with Area={area} and Width={width}:")
    print(f"- Calculated Length: {length}")

    #7. Square side 
    print("Side from area:", Shapes.calculate_square_side(area=64))        # 8.0
    print("Side from perimeter:", Shapes.calculate_square_side(perimeter=20))  # 5.0

if __name__ == "__main__":
    
    main()
