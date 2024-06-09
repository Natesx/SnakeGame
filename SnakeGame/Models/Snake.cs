namespace SnakeGame.Models
{
    public class Snake
    {
        public List<Pixel> SnakeBody { get; set; }

        public Snake(int areaWidth, int areaHeight)
        {
            SnakeBody = new List<Pixel>(){
                new Pixel // head
                {
                    X = areaWidth / 2,
                    Y = areaHeight / 2
                },
                new Pixel // body 1
                {
                    X = (areaWidth / 2) + 2,
                    Y = (areaHeight / 2)
                },
                new Pixel // body 2
                {
                    X = (areaWidth / 2) + 4,
                    Y = (areaHeight / 2)
                }
            };
        }
    }
}