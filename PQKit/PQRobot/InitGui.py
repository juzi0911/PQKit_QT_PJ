class MeshPartWorkbench ( Workbench ):
	"MeshPart workbench object"
	Icon = """
			/* XPM */
			static const char *MeshPart_Box[]={
			"16 16 3 1",
			". c None",
			"# c #000000",
			"a c #c6c642",
			"................",
			".......#######..",
			"......#aaaaa##..",
			".....#aaaaa###..",
			"....#aaaaa##a#..",
			"...#aaaaa##aa#..",
			"..#aaaaa##aaa#..",
			".########aaaa#..",
			".#aaaaa#aaaaa#..",
			".#aaaaa#aaaa##..",
			".#aaaaa#aaa##...",
			".#aaaaa#aa##....",
			".#aaaaa#a##... .",
			".#aaaaa###......",
			".########.......",
			"................"};
			"""
	MenuText = "MeshPart"
	ToolTip = "MeshPart workbench"

	def Initialize(self):
		# load the module
		import MeshPartGui
		import MeshPart
	def GetClassName(self):
		return "MeshPartGui::Workbench"

#Gui.addWorkbench(MeshPartWorkbench())
