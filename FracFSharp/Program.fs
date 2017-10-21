// 在 http://fsharp.org 上了解有关 F# 的详细信息
// 请参阅“F# 教程”项目以获取更多帮助。

#light
open System
open System.Drawing
open System.Windows.Forms
open System.Numerics


// 迭代次数
let maxIterations = 30

// 七种颜色
let colors = [| Color.Red; Color.Orange; Color.Yellow; 
                Color.Green; Color.Blue; Color.Indigo; 
                Color.Purple; |]

// 映射比例
let scalingFactor = 1.0 / 200.0
// 将像素映射为坐标
let mapPlane(x, y) =
    let fx = ((float x) * scalingFactor) - 2.0
    let fy = ((float y) * scalingFactor) - 1.0
    Complex(fx,fy)

let mutable iteration = 0
let mutable current = Complex( 0.0, 0.0)
let mutable temp = Complex( 0.0, 0.0)

let buildImage(w:int,h:int) =
    let image = new Bitmap(w, h)
    for x = 0 to image.Width - 1 do
        for y = 0 to image.Height - 1 do
            iteration <- 0
            current <- mapPlane(x, y)
            temp <- current
            // 判断当前点是否在Mandelbrot集合内
            while(temp.Magnitude <= 2.0 && iteration < maxIterations) do
                temp <- temp * temp + current
                iteration <- iteration + 1
            
            // 如果不在，像素为黑色    
            if iteration = maxIterations then
                image.SetPixel(x, y, Color.Black)
            else
                image.SetPixel(x, y, colors.[iteration % colors.Length])
    image

let form =
    let temp = new Form() in
    temp.Height <- 435
    temp.Width <- 600
    temp.Paint.Add(fun e -> e.Graphics.DrawImage(buildImage(temp.Width,temp.Height), 0, 0))
    temp.Text <- "Drawing Mandelbrot Set"
    temp
   
[<EntryPoint>][<STAThread>]
do Application.Run(form)


