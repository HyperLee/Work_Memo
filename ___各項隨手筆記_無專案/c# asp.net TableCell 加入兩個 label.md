在 C# ASP.NET 中，如果你想在一个 `TableCell` 中加入两个 `Label` 控件，可以按照以下步骤操作：

1. 创建 `TableCell` 对象。
2. 创建两个 `Label` 控件。
3. 将 `Label` 控件添加到 `TableCell` 的 `Controls` 集合中。

以下是一个示例代码：

```csharp
// 创建TableCell对象
TableCell cell = new TableCell();

// 创建第一个Label
Label label1 = new Label();
label1.Text = "Label 1 Text";

// 创建第二个Label
Label label2 = new Label();
label2.Text = "Label 2 Text";

// 将Label控件添加到TableCell中
cell.Controls.Add(label1);
cell.Controls.Add(new LiteralControl(" ")); // 添加一个空格，或使用其他的HTML标记来分隔
cell.Controls.Add(label2);

// 然后将TableCell添加到TableRow中
TableRow row = new TableRow();
row.Cells.Add(cell);

// 最后将TableRow添加到Table中
Table table = new Table();
table.Rows.Add(row);

// 添加table到页面中
this.Controls.Add(table);
```

这个代码会在同一个 `TableCell` 中放置两个 `Label` 控件，你可以根据需要调整 `Label` 的样式、文本等属性。使用 `LiteralControl` 可以在两个 `Label` 之间插入空格或其他分隔符。