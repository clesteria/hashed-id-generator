open System

let offset_basis = 0xcbf29ce484222325L
let FNV_prime = 0x100000001b3L
let base32 = "abcdefghijklmnopqrstuvwxyz234567"

let getFlv1a (string: string) =
    let bytesData = System.Text.Encoding.UTF8.GetBytes(string)
    let mutable hash = offset_basis

    for i in bytesData do
        hash <- (hash ^^^ (int64 i)) * FNV_prime

    hash

let getBase32 (bin: string) =
    let mutable hashstring = ""

    for i = 0 to 12 do
        let hash = System.Convert.ToInt32(int ("0b" + bin[(5 * i) .. (5 * i + 4)]))
        hashstring <- hashstring + base32[ hash ].ToString()

    hashstring

[<EntryPoint>]
let main args =
    let hash = getFlv1a args[0]
    let hashhex = (System.Convert.ToString(hash, 16))
    let hashbin = (System.Convert.ToString(hash, 2).PadLeft(65, '0'))

    printf "fnv1a64: %s\n" hashhex
    getBase32 hashbin |> printf "base32: %s\n"

    0
