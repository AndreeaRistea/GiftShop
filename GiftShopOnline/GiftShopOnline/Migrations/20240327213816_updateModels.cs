using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftShopOnline.Migrations
{
    /// <inheritdoc />
    public partial class updateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWishlist_Products_ProductsProductId",
                table: "ProductWishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWishlist_Wishlist_WishlistsWishlistId",
                table: "ProductWishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CartItems_ProductId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "WishlistId",
                table: "Wishlist",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "WishlistsWishlistId",
                table: "ProductWishlist",
                newName: "WishlistsId");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                table: "ProductWishlist",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWishlist_WishlistsWishlistId",
                table: "ProductWishlist",
                newName: "IX_ProductWishlist_WishlistsId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Carts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CartItemId",
                table: "CartItems",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWishlist_Products_ProductsId",
                table: "ProductWishlist",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWishlist_Wishlist_WishlistsId",
                table: "ProductWishlist",
                column: "WishlistsId",
                principalTable: "Wishlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CartItems_Id",
                table: "Products",
                column: "Id",
                principalTable: "CartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWishlist_Products_ProductsId",
                table: "ProductWishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWishlist_Wishlist_WishlistsId",
                table: "ProductWishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CartItems_Id",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Wishlist",
                newName: "WishlistId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "WishlistsId",
                table: "ProductWishlist",
                newName: "WishlistsWishlistId");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductWishlist",
                newName: "ProductsProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWishlist_WishlistsId",
                table: "ProductWishlist",
                newName: "IX_ProductWishlist_WishlistsWishlistId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Carts",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartItems",
                newName: "CartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWishlist_Products_ProductsProductId",
                table: "ProductWishlist",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWishlist_Wishlist_WishlistsWishlistId",
                table: "ProductWishlist",
                column: "WishlistsWishlistId",
                principalTable: "Wishlist",
                principalColumn: "WishlistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CartItems_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "CartItems",
                principalColumn: "CartItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
