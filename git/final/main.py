from Shapes import Shapes

def main():

    
    

    #global variable
    area = 50
    width = 5
    
    #6. Perimeter of The Square 
    shapes = Shapes()
    print("The perimeter of the square is:", shapes.perimeterOfSquare(8))


    #3. Rectangle
    print("The width of the rectangle is: ",Shapes.rectangle_width(area=50, length=10))

    
    
    # Task 4: Calculate the length of a rectangle
    length = Shapes.calculate_rectangle_length(area, width)
    print(f"Rectangle with Area={area} and Width={width}:")
    print(f"- Calculated Length: {length}")

    #7. Square side 
    print("Side from area:", Shapes.calculate_square_side(area=64))        # 8.0
    print("Side from perimeter:", Shapes.calculate_square_side(perimeter=20))  # 5.0

if __name__ == "__main__":
    
    main()
