from Shapes import Shapes

def main():
    area = 50
    width = 5
    
    # Task 4: Calculate the length of a rectangle
    length = Shapes.calculate_rectangle_length(area, width)
    print(f"Rectangle with Area={area} and Width={width}:")
    print(f"- Calculated Length: {length}")

if __name__ == "__main__":
    main()
