#$in = New-Object array$
$in = Get-Content .\sample.txt | ForEach-Object { ,$_.ToCharArray() }
$w = $in[0].Count
$h = $in.Count

$dx = @( -1, 0, 1, -1, 0, 1, -1, 0, 1 )
$dy = @( -1, -1, -1, 0, 0, 0, 1, 1, 1 )

$x = 0
$y = 0